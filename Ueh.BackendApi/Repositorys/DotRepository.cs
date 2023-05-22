using Ueh.BackendApi.Data.EF;
using Ueh.BackendApi.Data.Entities;

namespace Ueh.BackendApi.Repositorys
{
    public class DotRepository : IDotRepository
    {
        private readonly UehDbContext _context;

        public DotRepository(UehDbContext context)
        {
            _context = context;
        }
        public bool CreateDot(Dot dot)
        {
            _context.Add(dot);
            return Save();
        }

        public bool DeleteDot(Dot dot)
        {
            _context.Remove(dot);
            return Save();
        }

        public bool DotExists(string id)
        {
            return _context.Dots.Any(c => c.madot == id);
        }

        public ICollection<Dot> GetAllDot()
        {
            return _context.Dots.ToList();
        }



        public Dot GetDot(string id)
        {
            return _context.Dots.Where(e => e.madot == id).FirstOrDefault();

        }

        public ICollection<Sinhvien> GetSinhvienByDot(string dotId)
        {
            return _context.SinhvienDot.Where(e => e.madot == dotId).Select(c => c.sinhvien).ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateDot(Dot dot)
        {
            _context.Update(dot);
            return Save();
        }
    }
}
