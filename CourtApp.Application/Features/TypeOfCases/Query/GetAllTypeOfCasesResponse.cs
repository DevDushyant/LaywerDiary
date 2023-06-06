using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.Typeofcasess.Query
{
    public class GetAllTypeOfCasesResponse
    {
        public int Id { get; set; }
        public string CaseNature { get; set; }
        public string Typeofcases { get; set; }
    }
}
