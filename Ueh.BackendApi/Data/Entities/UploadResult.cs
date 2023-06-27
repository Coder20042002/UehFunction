namespace Ueh.BackendApi.Data.Entities
{
    public class UploadResult
    {
        public Guid Id { get; set; }
        public string Mssv { get; set; }
        public string FileType { get; set; }
        public string? FileName { get; set; }
        public string? StoredFileName { get; set; }
        public string? ContentType { get; set; }
    }
}
