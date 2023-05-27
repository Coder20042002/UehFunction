using Ueh.BackendApi.Data.Entities;

namespace Ueh.BackendApi.IRepositorys
{
    public interface ILoaiRepository
    {
        ICollection<Loai> GetLoai();

    }
}
