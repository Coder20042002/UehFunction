namespace Ueh.BackendApi.Data.Entities
{
    public class Chitiet
    {
        public Guid mapc { get; set; }
        public string? tencty { get; set; }
        public string? vitri { get; set; }
        public string? website { get; set; }
        public string? huongdan { get; set; }
        public string? chucvu { get; set; }
        public string? emailhd { get; set; }
        public string? sdthd { get; set; }
        public string? tendetai { get; set; }
        public string? status { get; set; }
        public Phancong phancong { get; set; }

    }
}
