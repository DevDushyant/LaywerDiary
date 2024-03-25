using AutoMapper;
using CourtApp.Application.Features.ProceedingHead;
using CourtApp.Application.Features.WorkMasterSub;
using CourtApp.Domain.Entities.LawyerDiary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.Mappings
{
    public class WorkMasterSubProfile: Profile
    {
        public WorkMasterSubProfile()
        {
            CreateMap<WorkMasterSubEntity, GetWorkMasterSubResponse>();
            CreateMap<WorkMasterSubCommand, WorkMasterSubEntity>();
        }
    }
}
