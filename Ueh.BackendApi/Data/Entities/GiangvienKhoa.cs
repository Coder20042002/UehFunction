namespace Ueh.BackendApi.Data.Entities
{
    public class GiangvienKhoa
    {
        public string magv { get; set; }
        public string makhoa { get; set; }
        public Giangvien giangvien { get; set; }
        public Khoa khoa { get; set; }
    }
}
