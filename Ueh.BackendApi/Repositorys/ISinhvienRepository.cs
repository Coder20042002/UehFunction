using Ueh.BackendApi.Data.Entities;
using Ueh.BackendApi.Dtos;

namespace Ueh.BackendApi.Repositorys
{
    public interface ISinhvienRepository
    {
        ICollection<Sinhvien> GetSinhviens();
        Sinhvien GetSinhvien(string mssv);
        Sinhvien GetSinhvienName(string name);
        Sinhvien GetSinhvienTrimToUpper(SinhvienDto sinhvienCreate);
        bool SinhvienExists(string mssv);
        bool CreateSinhvien(string madot, string maloai, Sinhvien sinhvien);
        bool UpdateSinhvien(string madot, string maloai, Sinhvien sinhvien);
        bool DeleteSinhvien(Sinhvien sinhvien);
        bool Save();
    }
}
