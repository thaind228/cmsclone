namespace CmsBase.Models
{
    public class StorageFile
    {
        public string Key { get; set; } = string.Empty;

        public string Url { get; set; } = string.Empty;

        public long? Size { get; set; }

        public DateTime? LastModified { get; set; }
    }
}