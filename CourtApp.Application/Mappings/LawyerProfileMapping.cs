using AutoMapper;
using CourtApp.Application.DTOs.Lawyer;
using CourtApp.Application.Features.Lawyer;
using CourtApp.Domain.Entities.LawyerDiary;

namespace CourtApp.Application.Mappings
{
    public class LawyerProfileMapping : Profile
    {
        public LawyerProfileMapping()
        {
            CreateMap<LawyerCreateCommand, LawyerMasterEntity>();
            CreateMap<LawyerMasterEntity, LawyerResponseById>();
            CreateMap<LawyerMasterEntity, LawyerResponse>()
                 .ForPath(d => d.Name,
                 opt => opt.MapFrom(src => src.FirstName + "" + src.MiddleName + "" + src.LastName));
        }
    }
}
