using AutoMapper;
using CourtApp.Application.Features.ProceedingHead;
using CourtApp.Application.Features.WorkMasterSub;
using CourtApp.Web.Areas.LawyerDiary.Models;

namespace CourtApp.Web.Areas.LawyerDiary.Mappings
{
    public class WorkMasterSubProfile:Profile
    {
        public WorkMasterSubProfile()
        {
            CreateMap<GetWorkMasterSubResponse, WorkMasterSubViewModel>().ReverseMap();
            CreateMap<WorkMasterSubCommand, WorkMasterSubViewModel>().ReverseMap();
        }
    }
}
