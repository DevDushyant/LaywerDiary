using AutoMapper;
using CourtApp.Application.DTOs.Case;
using CourtApp.Application.DTOs.CaseDetails;
using CourtApp.Application.Features.Case;
using CourtApp.Application.Features.CaseDetails;
using CourtApp.Web.Areas.Litigation.Models;
using System;
namespace CourtApp.Web.Areas.Litigation.Mappings
{
    public class CaseMappingProfile : Profile
    {
        public CaseMappingProfile()
        {
            //CreateMap<CaseViewModel, CreateCaseCommand>()
            //    .ForPath(d => d.CaseStageCode, s => s.MapFrom(src => src.CaseStageCode))
            //    .ForPath(d => d.LinkedCaseId, s => s.MapFrom(src => src.LinkedCaseId.Value))
            //    .ForPath(d => d.CourtComplexId, s => s.MapFrom(src => src.ComplexBenchId.Value))
            //    .ForPath(d => d.CourtDistrictId, s => s.MapFrom(src => src.CourtDistrictId.Value));
            //CreateMap<CaseViewModel, UpdateCaseDetailCommand>()
            //    .ForPath(d => d.CaseStageCode, s => s.MapFrom(src => src.CaseStageCode))
            //    .ForPath(d => d.LinkedCaseId, s => s.MapFrom(src => src.LinkedCaseId.Value))
            //    .ForPath(d => d.CourtComplexId, s => s.MapFrom(src => src.ComplexBenchId.Value))
            //    .ForPath(d => d.CourtDistrictId, s => s.MapFrom(src => src.CourtDistrictId.Value));

            CreateMap<CaseAgainstModel, CaseAgainstEntityModel>()
                .ForPath(d => d.CourtId, s => s.MapFrom(src => src.CourtId))
                .ReverseMap();
            CreateMap<CaseDetailResponse, GetCaseViewModel>()
                .ForPath(d => d.NextHearingDate, s => s.MapFrom(src => src.NextHearingDate.ToString("dd/MM/yyyy")));
            CreateMap<CaseHistoryResposnse, CaseHistoryViewModel>();
            CreateMap<CaseHistoryData, HistoryDetail>();
            CreateMap<CaseDocumentModel, DocumentAttachmentModel>();
            CreateMap<CaseUploadedDocument, CaseDoc>();
            CreateMap<CaseDetailInfoDto, CaseDetailInfoViewModel>();
            //CreateMap<UserCaseDetailResponse,CaseViewModel>()
            //     .ForPath(d => d.ComplexBenchId, s => s.MapFrom(src => src.CourtComplexId))
            //   ;

            CreateMap<GetCaseInfoDto, GetCaseInfoViewModel>();
            CreateMap<AgainstCaseDetail, AgainstCaseDecisionViewModel>();

            CreateMap<CaseAgainstModel, UpseartAgainstCaseDto>();


            #region CaseUpseartViewModel Model Mapping With Create COmmand & Update COmmand
            CreateMap<CaseUpseartViewModel, CreateCaseCommand>();
            CreateMap<CaseUpseartViewModel, UpdateCaseDetailCommand>();
            CreateMap<CaseAgainstModel, UpseartAgainstCaseDto>();
            CreateMap<UserCaseDetailResponse, CaseUpseartViewModel>();
            CreateMap<UpseartAgainstCaseDto, CaseAgainstModel>();
            #endregion

        }
    }
}
