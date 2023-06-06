using AutoMapper;
using CourtApp.Application.Features.CourtFeeStructure.Command;
using CourtApp.Application.Features.CourtFeeStructure.Queries;
using CourtApp.Web.Areas.LawyerDiary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourtApp.Web.Areas.LawyerDiary.Mappings
{
    public class CourtFeeStructProfile : Profile
    {
        public CourtFeeStructProfile()
        {
            CreateMap<GetCourtFeeStructureByIdResponse, CourtFeeStructureViewModel>().ReverseMap();
            CreateMap<GetAllCourtFeeStructureResponse, CourtFeeStructureViewModel>().ReverseMap();
            CreateMap<CreateCourtFeeStructureCommand, CourtFeeStructureViewModel>().ReverseMap();
            CreateMap<UpdateCourtFeeStructureCommand, CourtFeeStructureViewModel>().ReverseMap();
            CreateMap<DeleteCourtFeeStructureCommand, CourtFeeStructureViewModel>().ReverseMap();
        }
    }
}
