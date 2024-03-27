using AutoMapper;
using CourtApp.Application.Features.ProceedingHead;
using CourtApp.Application.Features.ProceedingSubHead;
using CourtApp.Web.Areas.LawyerDiary.Models;

namespace CourtApp.Web.Areas.LawyerDiary.Mappings
{
    public class ProceedingSubHeadProfile:Profile
    {
        public ProceedingSubHeadProfile()
        {
            CreateMap<GetProceedingSubHeadResponse, ProceedingSubHeadViewModel>().ReverseMap();
            CreateMap<ProceedingSubHeadCommand, ProceedingSubHeadViewModel>().ReverseMap();
        }
    }
}
