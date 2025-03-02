using AutoMapper;
using ScentifyAdmin.Models.Dtos;
using ScentifyAdmin.Models.Entities;

namespace ScentifyAdmin.Models.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Perfume, DtoPerfume>().ReverseMap()
                            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()));

        }
    }
}
