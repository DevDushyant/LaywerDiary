using AutoMapper;
using CourtApp.Application.Features.ProceedingHead;
using CourtApp.Application.Features.Publications.Command;
using CourtApp.Application.Features.Publications.Queries;
using CourtApp.Web.Areas.LawyerDiary.Models;

namespace CourtApp.Web.Areas.LawyerDiary.Mappings
{
    public class ProceedingHeadProfile:Profile
    {
        public ProceedingHeadProfile()
        {  
            CreateMap<GetProceedingHeadResponse, ProceedingHeadViewModel>().ReverseMap();
            CreateMap<ProceedingHeadCommand, ProceedingHeadViewModel>().ReverseMap();
        }
    }
}
