using AutoMapper;
using CourtApp.Application.Features.CourtMasters.Command;
using CourtApp.Application.Features.CourtMasters.Query;
using CourtApp.Web.Areas.LawyerDiary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourtApp.Web.Areas.LawyerDiary.Mappings
{
    public class CourtMasterMapping:Profile
    {
        public CourtMasterMapping()
        {
            CreateMap<GetCourtMasterDataAllResponse, CourtMasterViewModel>().ReverseMap();
            CreateMap<GetCourtMasterDataByIdResponse, CourtMasterViewModel>().ReverseMap();
            CreateMap<CreateCourtMasterCommand, CourtMasterViewModel>().ReverseMap();
            CreateMap<UpdateCourtMasterCommand, CourtMasterViewModel>().ReverseMap();
            CreateMap<DeleteCourtMasterCommand, CourtMasterViewModel>().ReverseMap();
           
        }
    }
}
