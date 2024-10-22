using AutoMapper;
using CourtApp.Application.DTOs.Case;
using CourtApp.Application.DTOs.CaseDetails;
using CourtApp.Application.Features.Case;
using CourtApp.Application.Features.CaseDetails;
using CourtApp.Domain.Entities.CaseDetails;
using System;

namespace CourtApp.Application.Mappings
{
    public class CaseMappingProfile : Profile
    {
        public CaseMappingProfile()
        {


            CreateMap<CreateCaseCommand, CaseDetailEntity>()
                 .ForPath(d => d.FTitleId, opt => opt.MapFrom(src => src.FirstTitleCode))
                 .ForPath(d => d.STitleId, opt => opt.MapFrom(src => src.SecoundTitleCode))
                 .ForPath(d => d.CaseStageId, opt => opt.MapFrom(src => src.CaseStageCode))
                 .ForPath(d => d.CourtDistrictId, opt => opt.MapFrom(src => src.CourtDistrictId))
                 .ForPath(d => d.CourtComplexId, opt => opt.MapFrom(src => src.CourtComplexId));

            CreateMap<UpdateCaseDetailCommand, CaseDetailEntity>()
                 .ForPath(d => d.FTitleId, opt => opt.MapFrom(src => src.FirstTitleCode))
                 .ForPath(d => d.STitleId, opt => opt.MapFrom(src => src.SecoundTitleCode))
                 .ForPath(d => d.CaseStageId, opt => opt.MapFrom(src => src.CaseStageCode))
                 .ForPath(d => d.CourtDistrictId, opt => opt.MapFrom(src => src.CourtDistrictId))
                 .ForPath(d => d.CourtComplexId, opt => opt.MapFrom(src => src.CourtComplexId));

            CreateMap<CaseAgainstEntityModel, CaseDetailAgainstEntity>()
                 .ForPath(d => d.CourtBenchId, opt => opt.MapFrom(src => src.AgainstBenchId != null ? src.AgainstBenchId.Value : Guid.Empty))
                 .ForPath(d => d.CourtComplexId, opt => opt.MapFrom(src => src.ComplexId!=null?src.ComplexId.Value:Guid.Empty))
                 .ForPath(d => d.CourtDistrictId, opt => opt.MapFrom(src => src.ComplexId != null ? src.CourtDistrictId.Value : Guid.Empty))
                 .ForPath(d => d.CnrNo, opt => opt.MapFrom(src => src.CnrNumber))
                 .ForPath(d => d.CaseNo, opt => opt.MapFrom(src => src.CaseNo))
                 .ForPath(d => d.CisYear, opt => opt.MapFrom(src => src.CisYear))
                 .ForPath(d => d.CisNo, opt => opt.MapFrom(src => src.CisNumber)).ReverseMap();

            CreateMap<CaseDetailEntity, CaseDetailResponse>();
            CreateMap<CaseDetailEntity, UserCaseDetailResponse>()
                 .ForPath(d => d.InstitutionDate, opt => opt.MapFrom(src => src.InstitutionDate))
                 .ForPath(d => d.FirstTitleCode, opt => opt.MapFrom(src => src.FTitleId))
                 .ForPath(d => d.SecoundTitleCode, opt => opt.MapFrom(src => src.STitleId))
                 .ForPath(d => d.SecoundTitleCode, opt => opt.MapFrom(src => src.STitleId))
                 .ForPath(d => d.StateId, opt => opt.MapFrom(src => src.StateId))
                 .ForPath(d => d.CaseStageCode, opt => opt.MapFrom(src => src.CaseStageId))
                 ;

            CreateMap<CaseDetailEntity, CaseDetailInfoDto>();


        }
    }
}
