namespace Ueh.BackendApi.Data.Entities
{
    public class SinhvienDot
    {
        public string mssv { get; set; }
        public string madot { get; set; }
        public Sinhvien sinhvien { get; set; }
        public Dot dot { get; set; }
    }
}