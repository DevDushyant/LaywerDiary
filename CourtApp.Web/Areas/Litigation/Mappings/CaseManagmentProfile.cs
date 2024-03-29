using AutoMapper;
using CourtApp.Application.Features.CaseManagment;
using CourtApp.Application.Features.WorkMaster;
using CourtApp.Web.Areas.LawyerDiary.Models;
using CourtApp.Web.Areas.Litigation.Models;
//using CourtApp.Application.Features.Commands.Cases;
//using CourtApp.Application.Features.Queries.Cases;
//using CourtApp.Web.Areas.Litigation.Models;

namespace CourtApp.Web.Areas.Litigation.Mappings
{
    public class CaseManagmentProfile : Profile
    {
        public CaseManagmentProfile()
        {
           // CreateMap<GetWorkMasterResponse, WorkMasterViewModel>().ReverseMap();
            CreateMap<CaseManagmentCommand, CaseViewModel>().ForMember(dest => dest.AgainstCaseDetails, conf => conf.MapFrom(src => src.AgainstCaseDetails)).ReverseMap();
            CreateMap<AgainstCaseDecision, AgainstCaseDecision1>().ReverseMap();
        }
    }
}
