﻿using AutoMapper;
using CourtApp.Application.Features.BooTypes.Queries.GetAllCached;
using CourtApp.Application.Features.Publications.Command;
using CourtApp.Application.Features.Publications.Queries;
using CourtApp.Web.Areas.LawyerDiary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourtApp.Web.Areas.LawyerDiary.Mappings
{
    public class PublicationMapping:Profile
    {
        public PublicationMapping()
        {
           
            CreateMap<GetPublicationByIdResponse, PublisherViewModel>().ReverseMap();
            CreateMap<GetAllPublisherCachedResponse, PublisherViewModel>().ReverseMap();
            CreateMap<CreatePublicationCommand, PublisherViewModel>().ReverseMap();
            CreateMap<UpdatePublicationCommand, PublisherViewModel>().ReverseMap();
            CreateMap<DeletePublicationCommand, PublisherViewModel>().ReverseMap();
        }
    }
}
