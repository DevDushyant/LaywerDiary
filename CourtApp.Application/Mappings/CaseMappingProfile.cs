using AutoMapper;
using CourtApp.Application.DTOs.Case;
using CourtApp.Application.Features.Case;
using CourtApp.Application.Features.WorkMaster;
using CourtApp.Domain.Entities.LawyerDiary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.Mappings
{
    public class CaseMappingProfile : Profile
    {
        public CaseMappingProfile()
        {
            CreateMap<CaseAgainstEntityModel, AgainstCaseDetails>().ReverseMap();
            CreateMap<CreateCaseCommand, CaseEntity>();
            CreateMap<CaseEntity, CaseDetailResponse>();
        }
    }
}
