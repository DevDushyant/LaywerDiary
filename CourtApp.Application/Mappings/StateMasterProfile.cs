using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CourtApp.Application.Features.States.Queries;
using CourtApp.Entities.Common;

namespace CourtApp.Application.Mappings
{
    public class StateMasterProfile: Profile
    {
        public StateMasterProfile()
        {
             CreateMap<GetStateMasterResponse, StateEntity>().ReverseMap();
        }
    }
}