namespace Ueh.BackendApi.Data.Entities
{
    public class Phancong
    {
        public Guid Id { get; set; }
        public string mssv { get; set; }
        public string magv { get; set; }
        public string madot { get; set; }
        public string maloai { get; set; }
        public string macn { get; set; }
        public string status { get; set; } = "true";
        public Giangvien Giangvien { get; set; }
        public Sinhvien Sinhvien { get; set; }
        public Dot dot { get; set; }
        public Loai loai { get; set; }
        public Chuyennganh chuyennganh { get; set; }
        public ICollection<Chitiet> chitiets { get; set; }
        public ICollection<Ketqua> ketquas { get; set; }

    }
}

