using AutoMapper;
using CourtApp.Application.Features.Clients.Commands;
using CourtApp.Application.Features.Clients.Queries.GetAllCached;
using CourtApp.Application.Features.Clients.Queries.GetById;
using CourtApp.Web.Areas.LawyerDiary.Models;

namespace CourtApp.Web.Areas.LawyerDiary.Mappings
{
    public class ClientMapping:Profile
    {
        public ClientMapping()
        {
            CreateMap<GetAllClientCachedResponse, ClientsViewModel>().ReverseMap();
            CreateMap<GetClientByIdResponse, ClientsViewModel>().ReverseMap();
            CreateMap<CreateClientCommand, ClientsViewModel>().ReverseMap();
            CreateMap<UpdateCreateClientCommand, ClientsViewModel>().ReverseMap();
        }
    }
}
