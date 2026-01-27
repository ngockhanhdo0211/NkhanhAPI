namespace NkhanhAPI.Models.Domain
{
    public class Image
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public string FileExtension { get; set; }
        public long FileSizeInBytes { get; set; }
        public string FilePath { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
