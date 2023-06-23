namespace Ueh.BackendApi.Data.Entities
{
    public class Dangky
    {
        public string mssv { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string magv { get; set; }
        public string makhoa { get; set; }
        public string status { get; set; } = "true";
        public Giangvien giangvien { get; set; }
        public Khoa khoa { get; set; }

    }
}