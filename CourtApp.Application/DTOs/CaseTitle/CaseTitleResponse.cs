using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.DTOs.CaseTitle
{
    public class CaseTitleResponse
    {
        public Guid Id { get; set; }
        public string title { get; set; }
        public string Case { get; set; }
        public string Type { get; set; }
    }
}
