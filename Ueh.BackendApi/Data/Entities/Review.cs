namespace Ueh.BackendApi.Data.Entities
{
    public class Review
    {
        public string Id { get; set; }
        public string noidung { get; set; }
        public DateTime ngay { get; set; }
        public Reviewer reviewer { get; set; }
        public Sinhvien sinhvien { get; set; }
    }
}