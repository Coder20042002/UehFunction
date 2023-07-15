namespace Ueh.BackendApi.Request
{
    public class KetquaRequest
    {
        public string TenSinhVien { get; set; }
        public string MaSinhVien { get; set; }
        public string Lop { get; set; }
        public string Khoa { get; set; }
        public string? Email { get; set; }

        public double Diem { get; set; }
    }
}
