﻿using AutoMapper;
using CourtApp.Application.Features.Districts.Queries;
using CourtApp.Web.Areas.LawyerDiary.Models;

namespace CourtApp.Web.Areas.LawyerDiary.Mappings
{
    public class DistrictMasterMapping:Profile
    {
        public DistrictMasterMapping()
        {
            CreateMap<GetDistrictResponse, DistrictViewModel>().ReverseMap();
        }
    }
}
