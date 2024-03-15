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
            CreateMap<CaseNatureByIdResponse, NatureEntity>().ReverseMap();
            CreateMap<CaseNatureAllCachedResponse, NatureEntity>().ReverseMap();
            CreateMap<CreateCaseNatureCommand, NatureEntity>().ReverseMap();
            CreateMap<UpdateCaseNatureCommand, NatureEntity>().ReverseMap();
            CreateMap<DeleteCaseNatureCommand, NatureEntity>().ReverseMap();
        }
    }
}
