using AutoMapper;
using CourtApp.Application.Features.Clients.Commands;
using CourtApp.Application.Features.Clients.Queries.GetAllCached;
using CourtApp.Application.Features.Clients.Queries.GetById;
using CourtApp.Domain.Entities.LawyerDiary;

namespace CourtApp.Application.Mappings
{
    public class ClientProfile : Profile
    {
        public ClientProfile()
        {
            CreateMap<CreateClientCommand, ClientEntity>()
                .ForPath(d => d.State.Code, opt => opt.MapFrom(src => src.StateCode))
                .ForPath(d => d.District.Code, opt => opt.MapFrom(src => src.DistrictCode));
            CreateMap<ClientEntity,GetClientByIdResponse>()
                .ForPath(d => d.StateCode, opt => opt.MapFrom(src => src.State.Code))
                .ForPath(d => d.DistrictCode, opt => opt.MapFrom(src => src.District.Code));

                        
            CreateMap<GetAllClientCachedResponse, ClientEntity>().ReverseMap();
        }
    }
}
