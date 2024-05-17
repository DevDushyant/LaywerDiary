using AutoMapper;
using CourtApp.Application.DTOs.Case;
using CourtApp.Web.Areas.Litigation.Models;

namespace CourtApp.Web.Areas.Litigation.Mappings
{
    public class HearingMappingProfile:Profile
    {
        public HearingMappingProfile()
        {
            CreateMap<CaseDetailResponse, HearingViewModel>();           
        }
    }
}
