using AutoMapper;
using CourtApp.Application.Features.CaseNatures.Command;
using CourtApp.Application.Features.CaseNatures.Query;
using CourtApp.Domain.Entities.LawyerDiary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.Mappings
{
    public class CaseNatureProfile:Profile
    {

        public CaseNatureProfile()
        {
            CreateMap<CaseNatureByIdResponse, CaseNatureEntity>().ReverseMap();
            CreateMap<CaseNatureAllCachedResponse, CaseNatureEntity>().ReverseMap();
            CreateMap<CreateCaseNatureCommand, CaseNatureEntity>().ReverseMap();
            CreateMap<UpdateCaseNatureCommand, CaseNatureEntity>().ReverseMap();
            CreateMap<DeleteCaseNatureCommand, CaseNatureEntity>().ReverseMap();
        }
    }
}
