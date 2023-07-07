namespace Ueh.BackendApi.Data.Entities
{
    public class Dangky
    {
        public string mssv { get; set; }
        public string ho { get; set; }
        public string ten { get; set; }
        public string lop { get; set; }
        public string email { get; set; }
        public string magv { get; set; }
        public string makhoa { get; set; }
        public string madot { get; set; }
        public string status { get; set; } = "true";
        public Giangvien giangvien { get; set; }
        public Khoa khoa { get; set; }

    }
}