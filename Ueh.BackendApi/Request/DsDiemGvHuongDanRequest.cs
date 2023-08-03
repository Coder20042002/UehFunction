namespace Ueh.BackendApi.Request
{
    public class DsDiemGvHuongDanRequest
    {
        public string tenkhoa { get; set; }
        public string tendot { get; set; }
        public string tenloai { get; set; }
        public string hotensv { get; set; }
        public string mssv { get; set; }
        public string? tendetai { get; set; }
        public string? tencty { get; set; }

        public string? malop { get; set; }
        public string? hotengv1 { get; set; }
        public string? hotengv2 { get; set; }
        public string? diemtong { get; set; }
        public string? ngaycham { get; set; }

    }
}
