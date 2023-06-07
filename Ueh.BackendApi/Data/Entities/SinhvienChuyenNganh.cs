namespace Ueh.BackendApi.Data.Entities
{
    public class SinhvienChuyenNganh
    {
        public string mssv { get; set; }
        public string macn { get; set; }
        public Sinhvien sinhvien { get; set; }
        public Chuyennganh chuyennganh { get; set; }
    }
}