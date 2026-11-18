
using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using CmsBase.Models;
using CmsBase.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace CmsBase.Services
{
    public class StorageService : IStorageService
    {
        private readonly StorageOptions _options;
        private readonly AmazonS3Client _client;

        public StorageService(IOptions<StorageOptions> options)
        {
            _options = options.Value;

            var credentials = new BasicAWSCredentials(_options.AccessKey, _options.SecretKey);

            var config = new AmazonS3Config
            {
                ServiceURL = _options.Endpoint,
                ForcePathStyle = true,
                Timeout = TimeSpan.FromMinutes(30)
            };

            _client = new AmazonS3Client(credentials, config);
        }

        // Chuẩn hóa prefix, luôn giới hạn trong thư mục gốc StorageOptions.Folder.
        // Prefix từ giao diện là đường dẫn tương đối, ví dụ: FAQ/ hoặc FAQ/Banner/.
        private string NormalizeRelativePrefix(string? prefix)
        {
            var root = (_options.Folder ?? "").Trim('/');

            if (string.IsNullOrWhiteSpace(root))
                throw new InvalidOperationException(
                    "Storage:Folder chưa được cấu hình.");

            prefix = (prefix ?? "").Trim();

            if (prefix.Contains('\\'))
                throw new ArgumentException("Đường dẫn thư mục không hợp lệ.");

            prefix = prefix.Trim('/');

            // Hỗ trợ cả prefix đầy đủ nếu nơi gọi cũ truyền vào.
            if (prefix.Equals(root, StringComparison.OrdinalIgnoreCase))
            {
                prefix = "";
            }
            else if (prefix.StartsWith(
                root + "/",
                StringComparison.OrdinalIgnoreCase))
            {
                prefix = prefix.Substring(root.Length + 1);
            }

            if (prefix.Length > 0)
            {
                var segments = prefix.Split('/');

                if (segments.Any(x =>
                    string.IsNullOrWhiteSpace(x) ||
                    x == "." ||
                    x == ".."))
                {
                    throw new ArgumentException(
                        "Đường dẫn thư mục không hợp lệ.");
                }
            }

            return prefix.Length == 0 ? "" : prefix + "/";
        }

        private string GetFullPrefix(string? prefix)
        {
            var root = _options.Folder.Trim('/');
            return root + "/" + NormalizeRelativePrefix(prefix);
        }

        public async Task<List<string>> UploadAsync(
            List<IFormFile> files,
            string? prefix = "")
        {
            const long MaxFileSize = 500L * 1024 * 1024;

            var allowExtensions = new[]
            {
                ".jpg", ".jpeg", ".png", ".gif", ".webp", ".mp4",
                ".pdf", ".doc", ".docx", ".xls", ".xlsx",
                ".zip", ".rar", ".apk"
            };

            var result = new List<string>();
            var fullPrefix = GetFullPrefix(prefix);

            using var transferUtility = new TransferUtility(_client);

            foreach (var file in files)
            {
                if (file == null || file.Length == 0)
                    continue;

                if (file.Length > MaxFileSize)
                    throw new Exception(
                        $"{file.FileName} vượt quá 500MB.");

                var extension =
                    Path.GetExtension(file.FileName).ToLowerInvariant();

                if (!allowExtensions.Contains(extension))
                    throw new Exception(
                        $"{file.FileName} không đúng định dạng.");

                var originalName = Path.GetFileNameWithoutExtension(file.FileName);
                var normalizedName = RemoveVietnameseSigns(originalName);
                var fileName = $"{normalizedName}{extension}";

                var key = fullPrefix + fileName;

                using var stream = file.OpenReadStream();

                var request = new TransferUtilityUploadRequest
                {
                    BucketName = _options.BucketName,
                    Key = key,
                    InputStream = stream,
                    ContentType = string.IsNullOrWhiteSpace(file.ContentType)
                        ? "application/octet-stream"
                        : file.ContentType
                };

                await transferUtility.UploadAsync(request);

                result.Add(key);
            }

            return result;
        }
        private string RemoveVietnameseSigns(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return text;

            text = text.Normalize(System.Text.NormalizationForm.FormD);

            var chars = text.Where(c =>
                System.Globalization.CharUnicodeInfo.GetUnicodeCategory(c)
                != System.Globalization.UnicodeCategory.NonSpacingMark).ToArray();

            text = new string(chars).Normalize(System.Text.NormalizationForm.FormC);
            text = text.Replace('đ', 'd').Replace('Đ', 'D');

            return System.Text.RegularExpressions.Regex.Replace(text, @"[^a-zA-Z0-9._-]", "");
        }
        public async Task<bool> DeleteAsync(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                return false;

            var root = _options.Folder.Trim('/');
            var allowedPrefix = root + "/";

            // Không cho xóa object nằm ngoài thư mục quản lý.
            if (!key.StartsWith(
                allowedPrefix,
                StringComparison.Ordinal))
            {
                return false;
            }

            if (key.Contains('\\') ||
                key.Split('/').Any(x => x == ".."))
            {
                return false;
            }

            var request = new DeleteObjectRequest
            {
                BucketName = _options.BucketName,
                Key = key
            };

            await _client.DeleteObjectAsync(request);
            return true;
        }

        public async Task<bool> DeleteFolderAsync(
            string folderName,
            string? parentPrefix = "")
        {
            folderName = (folderName ?? "").Trim();

            if (string.IsNullOrWhiteSpace(folderName))
                throw new ArgumentException("Tên thư mục không hợp lệ.");

            if (folderName.Length > 100 ||
                folderName.Contains('/') ||
                folderName.Contains('\\') ||
                folderName == "." ||
                folderName == ".." ||
                folderName.Any(char.IsControl))
            {
                throw new ArgumentException("Tên thư mục không hợp lệ.");
            }

            // Chỉ xóa thư mục con trong thư mục cha hiện tại.
            // GetFullPrefix tự giới hạn trong Storage:Folder.
            string targetPrefix =
                GetFullPrefix(parentPrefix) + folderName + "/";

            string? continuationToken = null;

            do
            {
                var listRequest = new ListObjectsV2Request
                {
                    BucketName = _options.BucketName,
                    Prefix = targetPrefix,
                    ContinuationToken = continuationToken
                };

                var listResponse =
                    await _client.ListObjectsV2Async(listRequest);

                var objects = (listResponse.S3Objects
                    ?? Enumerable.Empty<S3Object>())
                    .Where(x => !string.IsNullOrEmpty(x.Key))
                    .Select(x => new KeyVersion { Key = x.Key })
                    .ToList();

                if (objects.Count > 0)
                {
                    var deleteRequest = new DeleteObjectsRequest
                    {
                        BucketName = _options.BucketName,
                        Objects = objects,
                        Quiet = false
                    };

                    var deleteResponse =
                        await _client.DeleteObjectsAsync(deleteRequest);

                    if (deleteResponse.DeleteErrors != null &&
                        deleteResponse.DeleteErrors.Count > 0)
                    {
                        var errors = string.Join("; ",
                            deleteResponse.DeleteErrors.Select(
                                x => x.Key + ": " + x.Message));

                        throw new Exception(
                            "Xóa thư mục chưa hoàn tất: " + errors);
                    }
                }

                continuationToken = listResponse.NextContinuationToken;

            } while (!string.IsNullOrEmpty(continuationToken));

            return true;
        }


        // Chỉ lấy ảnh/file trực tiếp trong thư mục đang mở.
        // Các thư mục con được lấy riêng bằng GetFoldersAsync.
        public async Task<List<StorageFile>> GetFilesAsync(
            string? prefix = "")
        {
            var result = new List<StorageFile>();

            var request = new ListObjectsV2Request
            {
                BucketName = _options.BucketName,
                Prefix = GetFullPrefix(prefix),
                Delimiter = "/"
            };

            ListObjectsV2Response response;

            do
            {
                response = await _client.ListObjectsV2Async(request);

                foreach (var item in response.S3Objects
             ?? Enumerable.Empty<S3Object>())
                {
                    // Bỏ object đại diện thư mục.
                    if (string.IsNullOrEmpty(item.Key) ||
                        item.Key.EndsWith("/"))
                    {
                        continue;
                    }

                    result.Add(new StorageFile
                    {
                        Key = item.Key,
                        Url = $"{_options.PublicUrl.TrimEnd('/')}/{item.Key}",
                        Size = item.Size,
                        LastModified = item.LastModified
                    });
                }

                request.ContinuationToken =
                    response.NextContinuationToken;

            } while (response.IsTruncated == true);

            return result
                .OrderByDescending(x => x.LastModified)
                .ToList();
        }

        // Lấy tên các thư mục con trực tiếp của thư mục hiện tại.
        public async Task<List<string>> GetFoldersAsync(string? prefix = "")
        {
            var result = new List<string>();

            var request = new ListObjectsV2Request
            {
                BucketName = _options.BucketName,
                Prefix = GetFullPrefix(prefix),
                Delimiter = "/"
            };

            ListObjectsV2Response response;

            do
            {
                response = await _client.ListObjectsV2Async(request);

                foreach (var item in response.CommonPrefixes
                             ?? Enumerable.Empty<string>())
                {
                    if (string.IsNullOrEmpty(item))
                        continue;

                    var folderName = item
                        .TrimEnd('/')
                        .Split('/')
                        .LastOrDefault();

                    if (!string.IsNullOrEmpty(folderName))
                    {
                        result.Add(folderName);
                    }
                }

                request.ContinuationToken = response.NextContinuationToken;

            } while (response.IsTruncated == true);

            return result
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .OrderBy(x => x)
                .ToList();
        }

        // S3 không có thư mục vật lý; tạo object có key kết thúc bằng "/".
        public async Task<bool> CreateFolderAsync(
            string folderName,
            string? prefix = "")
        {
            folderName = (folderName ?? "").Trim();

            if (string.IsNullOrWhiteSpace(folderName))
                throw new ArgumentException("Tên thư mục không được để trống.");

            if (folderName.Length > 100 ||
                folderName.Contains('/') ||
                folderName.Contains('\\') ||
                folderName == "." ||
                folderName == ".." ||
                folderName.Any(char.IsControl))
            {
                throw new ArgumentException("Tên thư mục không hợp lệ.");
            }

            var key = GetFullPrefix(prefix) + folderName + "/";

            var request = new PutObjectRequest
            {
                BucketName = _options.BucketName,
                Key = key,
                ContentBody = ""
            };

            await _client.PutObjectAsync(request);
            return true;
        }
    }
}
