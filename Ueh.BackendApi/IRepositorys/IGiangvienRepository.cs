using Ueh.BackendApi.Data.Entities;
using Ueh.BackendApi.Dtos;
using Ueh.BackendApi.Request;

namespace Ueh.BackendApi.IRepositorys
{
    public interface IGiangvienRepository
    {
        Task<List<KetquaRequest>> GetDanhSachDiem(string madot, string magv);

        Task<List<Sinhvien>> GetSinhVienByGiangVien(string dot, string magv);
        Task<List<Giangvien>> GetGiangvienByKhoa(string makhoa);
        Task<ICollection<Giangvien>> GetGiangviens();
        Task<Giangvien> GetGiangvien(string magv);
        Task<Giangvien> GetGiangvienName(string name);
        Task<bool> GiangvienExists(string magv);
        Task<bool> CreateGiangvien(string makhoa, Giangvien Giangvien);
        Task<bool> UpdateGiangvien(Giangvien Giangvien);
        Task<bool> DeleteGiangvien(Giangvien Giangvien);
        Task<bool> ImportExcelFile(IFormFile formFile, string makhoa);
        Task<List<GiangvienRequest>> GetGiangVienAndSinhVienHuongDan(string makhoa);
        Task<bool> Save();
    }
}
