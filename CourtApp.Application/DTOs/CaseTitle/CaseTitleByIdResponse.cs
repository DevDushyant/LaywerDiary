using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.DTOs.CaseTitle
{
    public class CaseTitleByIdResponse
    {
        public  Guid Id { get; set; }
        public  Guid CaseId { get; set; }
        public int TypeId { get; set; }
        public required string Title { get; set; }
    }
}
