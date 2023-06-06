using AutoMapper;
using CourtApp.Application.Features.CaseNatures.Command;
using CourtApp.Application.Features.CaseNatures.Query;
using CourtApp.Web.Areas.LawyerDiary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourtApp.Web.Areas.LawyerDiary.Mappings
{
    public class CaseNatureProfile:Profile
    {
        public CaseNatureProfile()
        {
            CreateMap<CaseNatureByIdResponse, CaseNatureViewModel>().ReverseMap();
            CreateMap<CaseNatureAllCachedResponse, CaseNatureViewModel>().ReverseMap();
            CreateMap<CreateCaseNatureCommand, CaseNatureViewModel>().ReverseMap();
            CreateMap<UpdateCaseNatureCommand, CaseNatureViewModel>().ReverseMap();
            CreateMap<DeleteCaseNatureCommand, CaseNatureViewModel>().ReverseMap();
        }
    }
}
