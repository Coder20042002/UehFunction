namespace Ueh.BackendApi.Data.Entities
{
    public class Chuyennganh
    {
        public string macn { get; set; }
        public string tencn { get; set; }
        public ICollection<Giangvien> giangviens { get; set; }
    }
}
