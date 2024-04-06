using AutoMapper;
using CourtApp.Application.DTOs.CourtComplex;
using CourtApp.Application.Features.CourtComplex;
using CourtApp.Domain.Entities.LawyerDiary;

namespace CourtApp.Application.Mappings
{
    internal class CourtComplexProfile:Profile
    {
        public CourtComplexProfile()
        {
            CreateMap<CreateCourtComplexCommand,CourtComplexEntity>();
            CreateMap<CourtComplexEntity, CourtComplexResponse>();
            CreateMap<CourtComplexEntity, CourtComplexByIdResponse>();
        }
    }
}
