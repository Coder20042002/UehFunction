namespace Ueh.BackendApi.Data.Entities
{
    public class Phancong
    {
        public Guid Id { get; set; }
        public string mssv { get; set; }
        public string magv { get; set; }
        public string madot { get; set; }
        public string status { get; set; }
        public Giangvien giangvien { get; set; }
        public Sinhvien sinhvien { get; set; }
        public Dot dot { get; set; }
        public ICollection<Chitiet> chitiets { get; set; }
        public ICollection<Ketqua> ketquas { get; set; }
        public ICollection<Lichsu> lichsus { get; set; }


    }
}

