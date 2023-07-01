namespace Ueh.BackendApi.Data.Entities
{
    public class Chuyennganh
    {
        public string macn { get; set; }
        public string tencn { get; set; }
        public string makhoa { get; set; }
        public ICollection<Sinhvien> sinhviens { get; set; }

    }
}
