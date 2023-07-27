using Ueh.BackendApi.Data.Entities;

namespace Ueh.BackendApi.IRepositorys
{
    public interface IKhoaRepository
    {
        Task<ICollection<Khoa>> GetListKhoaNoAdmin();
        Task<bool> CreateKhoa(Khoa khoa);
        Task<bool> UpdateKhoa(Khoa khoa);
        Task<bool> DeleteKhoa(Khoa khoa);
        Task<bool> ImportExcelFile(IFormFile formFile);
        Task<ICollection<Sinhvien>> GetKhoaBySinhviens(string madot, string makhoa);
        Task<ICollection<Giangvien>> GetKhoaByGiangviens(string makhoa);
        Task<ICollection<Khoa>> GetListKhoa();
        Task<Khoa> GetKhoaById(string makhoa);
        Task<bool> KhoaExists(string makhoa);
        Task<bool> Save();
    }
}
