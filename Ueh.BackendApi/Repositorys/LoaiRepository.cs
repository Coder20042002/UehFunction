using Ueh.BackendApi.Data.EF;
using Ueh.BackendApi.Data.Entities;
using Ueh.BackendApi.IRepositorys;

namespace Ueh.BackendApi.Repositorys
{
    public class LoaiRepository : ILoaiRepository
    {
        private readonly UehDbContext _context;

        public LoaiRepository(UehDbContext context)
        {
            _context = context;
        }
        public ICollection<Loai> GetLoai()
        {
            return _context.Loais.OrderBy(l => l.maloai).ToList();
        }


    }
}
