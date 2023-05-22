using Ueh.BackendApi.Data.Entities;

namespace Ueh.BackendApi.Repositorys
{
    public interface IDotRepository
    {
        ICollection<Dot> GetAllDot();
        Dot GetDot(string id);
        ICollection<Sinhvien> GetSinhvienByDot(string dotId);
        bool DotExists(string id);
        bool CreateDot(Dot dot);
        bool UpdateDot(Dot dot);
        bool DeleteDot(Dot dot);
        bool Save();
    }
}
