using AutoMapper;
using CourtApp.Application.DTOs.Case;
using CourtApp.Application.DTOs.CaseDetails;
using CourtApp.Application.Features.Case;
using CourtApp.Domain.Entities.LawyerDiary;
using System;

namespace CourtApp.Application.Mappings
{
    public class CaseMappingProfile : Profile
    {
        public CaseMappingProfile()
        {
            

            CreateMap<CreateCaseCommand, CaseDetailEntity>();
            CreateMap<CaseDetailEntity, CaseDetailResponse>();
            CreateMap<CaseDetailEntity, UserCaseDetailResponse>();
        }
    }
}
