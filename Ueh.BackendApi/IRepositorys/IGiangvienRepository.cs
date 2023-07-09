using Ueh.BackendApi.Data.Entities;
using Ueh.BackendApi.Dtos;
using Ueh.BackendApi.Request;

namespace Ueh.BackendApi.IRepositorys
{
    public interface IGiangvienRepository
    {
        Task<List<KetquaRequest>> GetDanhSachDiem(string madot, string magv);

        Task<List<Sinhvien>> GetSinhVienByGiangVien(string dot, string magv);
        Task<List<GiangvienUpdateRequest>> GetGiangvienByKhoa(string makhoa);
        Task<GiangvienUpdateRequest> GetThongtinGiangvien(string magv);
        Task<Giangvien> GetGiangvienName(string name);
        Task<bool> GiangvienExists(string magv);
        Task<bool> CreateGiangvien(string makhoa, Giangvien Giangvien);
        Task<bool> UpdateGiangvien(GiangvienUpdateRequest Giangvien);
        Task<bool> DeleteGiangvien(string magv);
        Task<bool> ImportExcelFile(IFormFile formFile, string makhoa);
        Task<List<GiangvienRequest>> GetGiangVienAndSinhVienHuongDan(string madot, string makhoa);
        Task<bool> Save();
    }
}
