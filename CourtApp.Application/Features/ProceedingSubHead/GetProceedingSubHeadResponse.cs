using CourtApp.Domain.Entities.LawyerDiary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.ProceedingSubHead
{
    public class GetProceedingSubHeadResponse
    {
        public Guid Id { get; set; }
        public Guid PHeadId { get; set; }
        public string Name_En { get; set; }
        public string Name_Hn { get; set; }
        public virtual ProceedingHeadEntity ProceedingHead { get; set; }

    }
}
