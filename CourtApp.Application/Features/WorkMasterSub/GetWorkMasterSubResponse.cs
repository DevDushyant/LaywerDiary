using CourtApp.Domain.Entities.LawyerDiary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.WorkMasterSub
{
    public class GetWorkMasterSubResponse
    {
        public Guid Id { get; set; }
        public Guid WMasterId { get; set; }
        public string Name_En { get; set; }
        public string Name_Hn { get; set; }
        public virtual WorkMasterEntity WorkMaster { get; set; }

    }
}
