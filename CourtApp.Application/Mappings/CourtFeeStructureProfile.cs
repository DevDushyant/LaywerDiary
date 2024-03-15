using AutoMapper;
using CourtApp.Application.Features.CourtFeeStructure.Command;
using CourtApp.Application.Features.CourtFeeStructure.Queries;
using CourtApp.Domain.Entities.LawyerDiary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.Mappings
{
    public class CourtFeeStructureProfile : Profile
    {
        public CourtFeeStructureProfile()
        {
            CreateMap<CourtFeeStructureEntity, GetCourtFeeStructureByIdResponse>()
                .ForPath(d => d.StateName, opt => opt.MapFrom(src => src.State.Name_En));
            CreateMap<CreateCourtFeeStructureCommand, CourtFeeStructureEntity>()
                .ForPath(d => d.State.Id, opt => opt.MapFrom(src => src.StateCode));
        }
    }
}
