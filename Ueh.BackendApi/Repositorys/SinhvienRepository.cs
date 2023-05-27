using ServiceStack;
using Ueh.BackendApi.Data.EF;
using Ueh.BackendApi.Data.Entities;
using Ueh.BackendApi.Dtos;
using Ueh.BackendApi.IRepositorys;

namespace Ueh.BackendApi.Repositorys
{
    public class SinhvienRepository : ISinhvienRepository
    {
        private readonly UehDbContext _context;

        public SinhvienRepository(UehDbContext context)
        {
            _context = context;
        }
        public bool CreateSinhvien(string madot, string maloai, Sinhvien sinhvien)
        {
            var sinhvienloaiEntity = _context.Loais.Where(a => a.maloai == maloai).FirstOrDefault();
            var sinhviendotEntity = _context.Dots.Where(a => a.madot == madot).FirstOrDefault();

            var sinhvienloai = new SinhvienLoai()
            {
                loai = sinhvienloaiEntity,
                sinhvien = sinhvien,
            };

            _context.Add(sinhvienloai);

            var sinhviendot = new SinhvienDot()
            {
                dot = sinhviendotEntity,
                sinhvien = sinhvien,
            };

            _context.Add(sinhviendot);

            _context.Add(sinhvien);

            return Save();
        }

        public bool DeleteSinhvien(Sinhvien sinhvien)
        {
            sinhvien.status = "false";
            _context.Update(sinhvien);
            return Save();
        }

        public Sinhvien GetSinhvienTrimToUpper(SinhvienDto sinhvienCreate)
        {
            return GetSinhviens().Where(c => c.mssv.Trim().ToUpper() == sinhvienCreate.hoten.TrimEnd().ToUpper())
                .FirstOrDefault();
        }

        public Sinhvien GetSinhvien(string mssv)
        {
            return _context.Sinhviens.Where(s => s.mssv == mssv).FirstOrDefault();
        }

        public Sinhvien GetSinhvienName(string name)
        {
            return _context.Sinhviens.Where(s => s.hoten == name).FirstOrDefault();

        }

        public ICollection<Sinhvien> GetSinhviens()
        {
            return _context.Sinhviens.OrderBy(s => s.mssv).ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool SinhvienExists(string mssv)
        {
            return _context.Sinhviens.Any(s => s.mssv == mssv);
        }

        public bool UpdateSinhvien(string madot, string maloai, Sinhvien sinhvien)
        {
            _context.Update(sinhvien);
            return Save();
        }
    }
}
