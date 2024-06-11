using CourtApp.Application.DTOs.CaseDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.DTOs.Registers
{
    public class CopyDisposalResponse: CaseDetailDto
    {
        public string Reason { get; set; }
    }
}
