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
            CreateMap<Giangvien, GiangvienDto>();
            CreateMap<Review, ReviewDto>();
            CreateMap<Dangky, DangkyDto>();

        }
    }
}
