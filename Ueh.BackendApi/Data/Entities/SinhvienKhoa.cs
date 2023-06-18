namespace Ueh.BackendApi.Data.Entities
{
    public class SinhvienKhoa
    {
        public string mssv { get; set; }
        public string makhoa { get; set; }
        public Sinhvien sinhvien { get; set; }
        public Khoa khoa { get; set; }
    }
}