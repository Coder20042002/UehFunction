using Ueh.BackendApi.Data.Entities;

namespace Ueh.BackendApi.IRepositorys
{
    public interface IPhanCongRepository
    {
        bool ImportExcelFile(IFormFile formFile);
        byte[] ExportToExcel();
        ICollection<PhanCong> GetPhanCongs();
        bool Save();
    }
}
