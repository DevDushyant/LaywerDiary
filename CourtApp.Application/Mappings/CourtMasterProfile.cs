using AutoMapper;
using CourtApp.Application.Features.CourtMasters.Command;
using CourtApp.Application.Features.CourtMasters.Query;
using CourtApp.Domain.Entities.LawyerDiary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.Mappings
{
    public class CourtMasterProfile:Profile
    {
        public CourtMasterProfile()
        {
            CreateMap<CreateCourtMasterCommand, CourtMasterEntity>().ReverseMap();
            CreateMap<UpdateCourtMasterCommand, CourtMasterEntity>().ReverseMap();
            CreateMap<DeleteCourtMasterCommand, CourtMasterEntity>().ReverseMap();
            CreateMap<GetCourtMasterDataByIdResponse, CourtMasterEntity>().ReverseMap();
            CreateMap<GetCourtMasterDataAllResponse, CourtMasterEntity>().ReverseMap();
            
        }
    }
}
