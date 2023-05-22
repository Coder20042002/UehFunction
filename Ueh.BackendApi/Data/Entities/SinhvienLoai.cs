namespace Ueh.BackendApi.Data.Entities
{
    public class SinhvienLoai
    {
        public string mssv { get; set; }
        public string maloai { get; set; }
        public Sinhvien sinhvien { get; set; }
        public Loai loai { get; set; }
    }
}