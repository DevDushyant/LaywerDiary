using AutoMapper;
using CourtApp.Application.Features.Clients.Commands;
using CourtApp.Application.Features.Clients.Queries.GetAllCached;
using CourtApp.Application.Features.Clients.Queries.GetById;
using CourtApp.Web.Areas.Client.Model;

namespace CourtApp.Web.Areas.LawyerDiary.Mappings
{
    public class ClientMapping:Profile
    {
        public ClientMapping()
        {
            CreateMap<GetAllClientCachedResponse, ClientViewModel>().ReverseMap();
            CreateMap<GetClientByIdResponse, ClientViewModel>().ReverseMap();
            CreateMap<CreateClientCommand, ClientViewModel>().ReverseMap();
            CreateMap<UpdateCreateClientCommand, ClientViewModel>().ReverseMap();
        }
    }
}
