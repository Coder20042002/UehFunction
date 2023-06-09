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
            CreateMap<ReviewDto, Review>();
            CreateMap<ReviewerDto, Reviewer>();
            CreateMap<Review, ReviewDto>();
            CreateMap<Reviewer, ReviewerDto>();
            CreateMap<Dangky, DangkyDto>();
            CreateMap<DangkyDto, Dangky>();
            CreateMap<PhanCong, PhanCongDto>();
            CreateMap<PhanCongDto, PhanCong>();
            CreateMap<Chuyennganh, ChuyennganhDto>();
            CreateMap<ChuyennganhDto, Chuyennganh>();



        }
    }
}
