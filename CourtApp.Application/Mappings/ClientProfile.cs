using AutoMapper;
using CourtApp.Application.Features.Clients.Commands;
using CourtApp.Application.Features.Clients.Queries.GetAllCached;
using CourtApp.Application.Features.Clients.Queries.GetById;
using CourtApp.Domain.Entities.Account;
using CourtApp.Domain.Entities.LawyerDiary;

namespace CourtApp.Application.Mappings
{
    public class ClientProfile : Profile
    {
        public ClientProfile()
        {          
            CreateMap<CreateClientCommand, ClientEntity>();
            CreateMap<ClientFee, CaseFeeEntity>()
            .ForPath(d => d.AdvanceAmount, s => s.MapFrom(src => src.FeeAdvance))
            .ForPath(d => d.SettledAmount, s => s.MapFrom(src => src.FeeSettled));
            CreateMap<ClientEntity, GetClientByIdResponse>();
            CreateMap<GetAllClientCachedResponse, ClientEntity>().ReverseMap();
        }
    }
}
