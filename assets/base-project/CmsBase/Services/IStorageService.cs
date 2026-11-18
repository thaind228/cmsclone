using CmsBase.Models;
using Microsoft.AspNetCore.Http;

namespace CmsBase.Services
{
    public interface IStorageService
    {
        Task<List<string>> UploadAsync(
            List<IFormFile> files,
            string? prefix = "");

        Task<bool> DeleteAsync(string key);

        Task<List<StorageFile>> GetFilesAsync(
            string? prefix = "");

        Task<List<string>> GetFoldersAsync(
            string? prefix = "");

        Task<bool> CreateFolderAsync(
            string folderName,
            string? prefix = "");
        Task<bool> DeleteFolderAsync(string folderName, string? parentPrefix = "");
    }
}