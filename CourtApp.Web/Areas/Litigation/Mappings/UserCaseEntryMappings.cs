using AutoMapper;
using CourtApp.Application.Features.Commands.Cases;
using CourtApp.Application.Features.Queries.Cases;
using CourtApp.Web.Areas.Litigation.Models;

namespace CourtApp.Web.Areas.Litigation.Mappings
{
    public class UserCaseEntryMappings : Profile
    {
        public UserCaseEntryMappings()
        {
            CreateMap<CommandCreateCaseEntry, CaseViewModel>().ReverseMap();
            CreateMap<CommandUpdateCaseEntry, CaseViewModel>().ReverseMap();
            CreateMap<CommandDeleteCaseEntry, CaseViewModel>().ReverseMap();
            CreateMap<ResponseGetAllCaseEntry, CaseViewModel>().ReverseMap();
           
        }
    }
}
