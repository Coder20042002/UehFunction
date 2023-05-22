using Ueh.BackendApi.Data.Entities;

namespace Ueh.BackendApi.Repositorys
{
    public interface ILoaiRepository
    {
        ICollection<Loai> GetLoai();

    }
}
