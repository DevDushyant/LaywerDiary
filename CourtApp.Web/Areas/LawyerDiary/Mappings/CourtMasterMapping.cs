using AutoMapper;
using CourtApp.Application.DTOs.CourtMaster;
using CourtApp.Application.Features.CourtMasters.Command;
using CourtApp.Web.Areas.LawyerDiary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourtBench = CourtApp.Web.Areas.LawyerDiary.Models.CourtBench;

namespace CourtApp.Web.Areas.LawyerDiary.Mappings
{
    public class CourtMasterMapping : Profile
    {
        public CourtMasterMapping()
        {
            CreateMap<CourtMasterViewModel, CreateCourtMasterCommand>();
               

            CreateMap<GetCourtMasterDataAllResponse, CourtMasterViewModel>().ReverseMap();
            CreateMap<GetCourtMasterDataByIdResponse, CourtMasterViewModel>();

            CreateMap<UpdateCourtMasterCommand, CourtMasterViewModel>().ReverseMap();
            CreateMap<DeleteCourtMasterCommand, CourtMasterViewModel>().ReverseMap();
            CreateMap<CourtBench, Application.DTOs.CourtMaster.CourtBenchResponse>();

        }
    }
}
