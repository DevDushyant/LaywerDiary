using AutoMapper;
using CourtApp.Application.Features.CaseManagment;
using CourtApp.Application.Features.WorkMaster;
using CourtApp.Domain.Entities.LawyerDiary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.Mappings
{
    public class CaseManagmentProfile : Profile
    {
        public CaseManagmentProfile()
        {
            //CreateMap<CaseEntity, GetWorkMasterResponse>();
            CreateMap<CaseManagmentCommand, CaseEntity>().ForMember(dest => dest.AgainstCaseDetails, conf => conf.MapFrom(src => src.AgainstCaseDetails));
            CreateMap<AgainstCaseDecision, AgainstCaseDetails>();
        }
    }
}
