using AutoMapper;
using Ueh.BackendApi.Data.Entities;
using Ueh.BackendApi.Dtos;

namespace Ueh.BackendApi.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Loai, LoaiDto>();
            CreateMap<Dot, DotDto>();
            CreateMap<DotDto, Dot>();
            CreateMap<Khoa, KhoaDto>();
            CreateMap<Sinhvien, SinhvienDto>();
            CreateMap<SinhvienDto, Sinhvien>();
            CreateMap<Giangvien, GiangvienDto>();
            CreateMap<GiangvienDto, Giangvien>();
            CreateMap<LichsuDto, Lichsu>();
            CreateMap<Lichsu, LichsuDto>();
            CreateMap<Dangky, DangkyDto>();
            CreateMap<DangkyDto, Dangky>();
            CreateMap<Phancong, PhancongDto>();
            CreateMap<PhancongDto, Phancong>();
            CreateMap<Chuyennganh, ChuyennganhDto>();
            CreateMap<ChuyennganhDto, Chuyennganh>();
            CreateMap<Chitiet, ChitietDto>();
            CreateMap<ChitietDto, Chitiet>();
            CreateMap<Ketqua, KetquaDto>();
            CreateMap<KetquaDto, Ketqua>();
            CreateMap<ChamcheoDto, Chamcheo>();
            CreateMap<SinhvienKhoa, SinhvienkhoaDto>();
            CreateMap<GiangvienKhoa, GiangvienkhoaDto>();
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();

        }
    }
}
