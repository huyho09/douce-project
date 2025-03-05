using AutoMapper;
using ScentifyWebApp.Models.Dtos;
using ScentifyWebApp.Models.Entities;

namespace ScentifyWebApp.Models.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Perfume, DtoPerfume>().ReverseMap()
                            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()));

			CreateMap<Invoice, DtoInvoice>().ReverseMap()
							.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()));
		}
    }
}
