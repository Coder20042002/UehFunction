using Ueh.BackendApi.Data.Entities;
using Ueh.BackendApi.Dtos;

namespace Ueh.BackendApi.IRepositorys
{
    public interface IGiangvienRepository
    {
        ICollection<Giangvien> GetGiangviens();
        Giangvien GetGiangvien(string magv);
        Giangvien GetGiangvienName(string name);
        Giangvien GetGiangvienTrimToUpper(GiangvienDto GiangvienCreate);
        bool GiangvienExists(string magv);
        bool CreateGiangvien(Giangvien Giangvien);
        bool UpdateGiangvien(Giangvien Giangvien);
        bool DeleteGiangvien(Giangvien Giangvien);
        bool Save();
    }
}
