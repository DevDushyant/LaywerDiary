using AutoMapper;
using CourtApp.Application.DTOs.Case;
using CourtApp.Application.DTOs.CaseDetails;
using CourtApp.Application.Features.Case;
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
            CreateMap<CaseHistoryResposnse, CaseHistoryViewModel>();
            CreateMap<CaseHistoryData, HistoryDetail>();
            //CreateMap<CommandDeleteCaseEntry, CaseViewModel>().ReverseMap();
            //CreateMap<ResponseGetAllCaseEntry, CaseViewModel>().ReverseMap();           
        }
    }
}
