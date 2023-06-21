namespace Ueh.BackendApi.Data.Entities
{
    public class Ketqua
    {
        public Guid mapc { get; set; }
        public float? tieuchi1 { get; set; }
        public float? tieuchi2 { get; set; }
        public float? tieuchi3 { get; set; }
        public float? tieuchi4 { get; set; }
        public float? tieuchi5 { get; set; }
        public float? tieuchi6 { get; set; }
        public float? tieuchi7 { get; set; }
        public float? diemGV { get; set; }
        public float? diemDN { get; set; }

        public Phancong phancong { get; set; }

    }
}
