using AutoMapper;
using CourtApp.Application.DTOs.Case;
using CourtApp.Application.Features.Case;
using CourtApp.Application.Features.UserCase;
using CourtApp.Web.Areas.Litigation.Models;
namespace CourtApp.Web.Areas.Litigation.Mappings
{
    public class CaseMappingProfile : Profile
    {
        public CaseMappingProfile()
        {
            CreateMap<CaseViewModel, CreateCaseCommand>();
            CreateMap<CaseAgainstModel, CaseAgainstEntityModel>();
            CreateMap<CaseDetailResponse, GetCaseViewModel>();
            //CreateMap<CommandUpdateCaseEntry, CaseViewModel>().ReverseMap();
            //CreateMap<CommandDeleteCaseEntry, CaseViewModel>().ReverseMap();
            //CreateMap<ResponseGetAllCaseEntry, CaseViewModel>().ReverseMap();
           
        }
    }
}
