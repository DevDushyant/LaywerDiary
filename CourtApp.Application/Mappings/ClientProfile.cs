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
            CreateMap<CreateClientCommand, ClientEntity>().ReverseMap();
            CreateMap<GetClientByIdResponse, ClientEntity>().ReverseMap();
            CreateMap<GetAllClientCachedResponse, ClientEntity>().ReverseMap();
        }
    }
}
