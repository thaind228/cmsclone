using CmsBase.Controllers.Common;
using CmsBase.Services;
using Microsoft.AspNetCore.Mvc;


namespace CmsBase.Controllers
{
    [RequireCmsSession]
    [Route("[controller]/[action]")]
    public class StorageController : Controller
    {
        private static readonly HashSet<string> ImageExtensions = new(StringComparer.OrdinalIgnoreCase)
        {
            ".jpg", ".jpeg", ".png", ".gif", ".webp", ".svg", ".avif", ".bmp", ".ico", ".apng", ".jfif"
        };
        private readonly IStorageService _storageService;
        private readonly ILogger<StorageController> _logger;

        public StorageController(
            IStorageService storageService,
            ILogger<StorageController> logger)
        {
            _storageService = storageService;
            _logger = logger;
        }
        [HttpGet]
        public IActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RequestSizeLimit(524288000)]
        [RequestFormLimits(MultipartBodyLengthLimit = 524288000)]
        public async Task<IActionResult> Upload(
    List<IFormFile> files,
    string? prefix = "")
        {
            try
            {
                if (files == null || files.Count == 0)
                {
                    return Json(new
                    {
                        success = false,
                        message = "Không có file upload."
                    });
                }

                var result = await _storageService.UploadAsync(files, prefix);

                return Json(new
                {
                    success = true,
                    files = result,
                    count = result.Count
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Upload");

                return Json(new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }


        [HttpGet]
        public async Task<IActionResult> GetFiles(
            string? prefix = "",
            int page = 1,
            string? keyword = "",
            bool imageOnly = false)
        {
            try
            {
                const int pageSize = 60;

                if (page < 1)
                    page = 1;

                var files = await _storageService.GetFilesAsync(prefix);
                if (imageOnly)
                    files = files.Where(file => IsImage(file.Key)).ToList();

                if (!string.IsNullOrWhiteSpace(keyword))
                {
                    files = files
                        .Where(x => x.Key.Contains(
                            keyword.Trim(),
                            StringComparison.OrdinalIgnoreCase))
                        .ToList();
                }

                files = files
                    .OrderByDescending(x => x.LastModified)
                    .ToList();

                var total = files.Count;

                var pagedFiles = files
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();

                // Chỉ lấy thư mục con khi ở trang đầu và không tìm kiếm.
                var folders = page == 1 &&
                              string.IsNullOrWhiteSpace(keyword)
                    ? await _storageService.GetFoldersAsync(prefix)
                    : new List<string>();

                return Json(new
                {
                    success = true,
                    files = pagedFiles.Select(file => new
                    {
                        file.Key, file.Url, file.Size, file.LastModified, IsImage = IsImage(file.Key)
                    }),
                    folders,
                    count = pagedFiles.Count,
                    hasMore = page * pageSize < total
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetFiles");

                return Json(new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetFolders(string? prefix = "")
        {
            try
            {
                var folders = await _storageService.GetFoldersAsync(prefix);

                return Json(new
                {
                    success = true,
                    folders
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetFolders");

                return Json(new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateFolder(
            string folderName,
            string? prefix = "")
        {
            try
            {
                await _storageService.CreateFolderAsync(folderName, prefix);

                return Json(new
                {
                    success = true,
                    message = "Tạo thư mục thành công."
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "CreateFolder");

                return Json(new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string key)
        {
            try
            {
                var result = await _storageService.DeleteAsync(key);

                return Json(new
                {
                    success = result
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Delete");

                return Json(new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteFolder(
            string folderName,
            string? prefix = "")
        {
            try
            {
                var result = await _storageService.DeleteFolderAsync(
                    folderName, prefix);

                return Json(new
                {
                    success = result,
                    message = result
                        ? "Đã xóa thư mục và toàn bộ file bên trong."
                        : "Xóa thư mục thất bại."
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "DeleteFolder");

                return Json(new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }

        [HttpGet]
        public IActionResult ImagePicker(bool imageOnly = false, bool dialog = false)
        {
            ViewData["ImageOnly"] = imageOnly;
            ViewData["PickerDialog"] = dialog;
            return View();
        }

        private static bool IsImage(string key) => ImageExtensions.Contains(Path.GetExtension(key));
    }
}
