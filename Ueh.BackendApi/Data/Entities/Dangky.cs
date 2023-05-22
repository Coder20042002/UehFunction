namespace Ueh.BackendApi.Data.Entities
{
    public class Dangky
    {
        public string Id { get; set; }
        public string mssv { get; set; }
        public string magv { get; set; }
        public string maloai { get; set; }
        public string madot { get; set; }

        public Sinhvien sinhvien { get; set; }
        public Giangvien giangvien { get; set; }
        public Loai loai { get; set; }
        public Dot dot { get; set; }
    }
}