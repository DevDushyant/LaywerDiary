using AutoMapper;
using CourtApp.Application.DTOs.Case;
using CourtApp.Application.DTOs.CaseDetails;
using CourtApp.Application.Features.Case;
using CourtApp.Application.Features.CaseDetails;
using CourtApp.Web.Areas.Litigation.Models;
namespace CourtApp.Web.Areas.Litigation.Mappings
{
    public class CaseMappingProfile : Profile
    {
        public CaseMappingProfile()
        {
            CreateMap<CaseViewModel, CreateCaseCommand>()
                .ForPath(d => d.CaseStageCode, s => s.MapFrom(src => src.CaseStageCode.Value))
                .ForPath(d => d.LinkedCaseId, s => s.MapFrom(src => src.LinkedCaseId.Value));
            CreateMap<CaseAgainstModel, CaseAgainstEntityModel>();
            CreateMap<CaseDetailResponse, GetCaseViewModel>()
                .ForPath(d => d.NextHearingDate, s => s.MapFrom(src => src.NextHearingDate.ToString("dd/MM/yyyy")));
            CreateMap<CaseHistoryResposnse, CaseHistoryViewModel>();
            CreateMap<CaseHistoryData, HistoryDetail>();
            CreateMap<CaseDocumentModel, DocumentAttachmentModel>();
            CreateMap<CaseUploadedDocument, CaseDoc>();
            CreateMap<CaseDetailInfoDto, CaseDetailInfoViewModel>();
            //CreateMap<CommandDeleteCaseEntry, CaseViewModel>().ReverseMap();
            //CreateMap<ResponseGetAllCaseEntry, CaseViewModel>().ReverseMap();           
        }
    }
}
