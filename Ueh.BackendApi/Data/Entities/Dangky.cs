namespace Ueh.BackendApi.Data.Entities
{
    public class Dangky
    {
        public string mssv { get; set; }
        public string hotensv { get; set; }
        public string magv { get; set; }
        public string maloai { get; set; }
        public string status { get; set; } = "true";
        public Giangvien giangvien { get; set; }
        public Loai loai { get; set; }
    }
}