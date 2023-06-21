﻿using Ueh.BackendApi.Data.Entities;
using Ueh.BackendApi.Dtos;

namespace Ueh.BackendApi.IRepositorys
{
    public interface IGiangvienRepository
    {
        Task<ICollection<Giangvien>> GetGiangviens();
        Task<Giangvien> GetGiangvien(string magv);
        Task<Giangvien> GetGiangvienName(string name);
        Task<bool> GiangvienExists(string magv);
        Task<bool> CreateGiangvien(string makhoa, Giangvien Giangvien);
        Task<bool> UpdateGiangvien(Giangvien Giangvien);
        Task<bool> DeleteGiangvien(Giangvien Giangvien);
        Task<bool> ImportExcelFile(string makhoa, IFormFile formFile);

        Task<bool> Save();
    }
}
