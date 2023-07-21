using Ueh.BackendApi.Data.Entities;

namespace Ueh.BackendApi.IRepositorys
{
    public interface IKhoaRepository
    {
        Task<ICollection<Sinhvien>> GetKhoaBySinhviens(string madot, string makhoa);
        Task<ICollection<Giangvien>> GetKhoaByGiangviens(string makhoa);

    }
}
