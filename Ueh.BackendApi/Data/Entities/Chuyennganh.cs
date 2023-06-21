namespace Ueh.BackendApi.Data.Entities
{
    public class Chuyennganh
    {
        public string macn { get; set; }
        public string tencn { get; set; }
        public ICollection<Phancong> phancongs { get; set; }
        public ICollection<Sinhvien> sinhviens { get; set; }

    }
}
