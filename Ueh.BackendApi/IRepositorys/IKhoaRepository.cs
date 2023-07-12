using Ueh.BackendApi.Data.Entities;

namespace Ueh.BackendApi.IRepositorys
{
    public interface IKhoaRepository
    {
        Task<ICollection<SinhvienKhoa>> GetKhoaBySinhviens(string madot, string makhoa);
        Task<ICollection<GiangvienKhoa>> GetKhoaByGiangviens(string makhoa);

    }
}
