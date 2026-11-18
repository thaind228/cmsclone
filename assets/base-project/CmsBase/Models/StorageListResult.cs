namespace CmsBase.Models.Storage
{
    public class StorageListResult
    {
        public bool Success { get; set; }

        public List<StorageFile> Files { get; set; } = new();

        public int Count => Files.Count;

        public string? Message { get; set; }
    }
}