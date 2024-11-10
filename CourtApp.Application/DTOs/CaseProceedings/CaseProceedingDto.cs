using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.DTOs.CaseProceedings
{
    public class CaseProceedingDto
    {
        public Guid CaseId { get; set; }
        public Guid HeadId { get; set; }
        public Guid SubHeadId { get; set; }
        public Guid? StageId { get; set; }
        public DateTime? NextDate { get; set; }
        public string Remark { get; set; }
        public ProceedingWorkDto ProcWork { get; set; }

    }
}
