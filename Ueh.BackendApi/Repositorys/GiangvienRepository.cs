using Ueh.BackendApi.Data.EF;
using Ueh.BackendApi.Data.Entities;
using Ueh.BackendApi.Dtos;

namespace Ueh.BackendApi.Repositorys
{
    public class GiangvienRepository : IGiangvienRepository
    {
        private readonly UehDbContext _context;

        public GiangvienRepository(UehDbContext context)
        {
            _context = context;
        }
        public bool CreateGiangvien(Giangvien Giangvien)
        {
            var giangvienkhoa = _context.Khoas.Where(a => a.makhoa == Giangvien.makhoa).FirstOrDefault();
            var giangvienchuyennganh = _context.Chuyennganhs.Where(a => a.macn == Giangvien.macn).FirstOrDefault();
            var giangvienreviewer = new Reviewer()
            {
                id = Giangvien.magv,
                hoten = Giangvien.tengv
            };
            _context.Add(giangvienreviewer);

            if (giangvienkhoa != null && giangvienchuyennganh != null)
                _context.Add(Giangvien);

            return Save();
        }

        public bool DeleteGiangvien(Giangvien Giangvien)
        {
            Giangvien.status = "false";
            _context.Update(Giangvien);
            return Save();
        }

        public Giangvien GetGiangvienTrimToUpper(GiangvienDto GiangvienCreate)
        {
            return GetGiangviens().Where(c => c.magv.Trim().ToUpper() == GiangvienCreate.tengv.TrimEnd().ToUpper())
                .FirstOrDefault();
        }

        public Giangvien GetGiangvien(string magv)
        {
            return _context.Giangviens.Where(s => s.magv == magv).FirstOrDefault();
        }

        public Giangvien GetGiangvienName(string name)
        {
            return _context.Giangviens.Where(s => s.tengv == name).FirstOrDefault();

        }

        public ICollection<Giangvien> GetGiangviens()
        {
            return _context.Giangviens.OrderBy(s => s.magv).ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool GiangvienExists(string magv)
        {
            return _context.Giangviens.Any(s => s.magv == magv);
        }

        public bool UpdateGiangvien(Giangvien Giangvien)
        {
            var giangvienkhoa = _context.Khoas.Where(a => a.makhoa == Giangvien.makhoa).FirstOrDefault();
            var giangvienchuyennganh = _context.Chuyennganhs.Where(a => a.macn == Giangvien.macn).FirstOrDefault();

            if (giangvienkhoa != null && giangvienchuyennganh != null)
                _context.Update(Giangvien);
            return Save();
        }
    }
}

