using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CourtApp.Application.Features.States.Queries;
using CourtApp.Web.Areas.LawyerDiary.Models;

namespace CourtApp.Web.Areas.LawyerDiary.Mappings
{
    public class StateMasterMapping:Profile
    {
        public StateMasterMapping()
        {
            CreateMap<GetStateMasterResponse, StateViewModel>().ReverseMap();
        }
    }
}