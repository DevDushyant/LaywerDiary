using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.Typeofcasess.Query
{
    public class TypeOfCasesQueryByIdResponse
    {
        public int Id { get; set; }
        public int CaseNatureId { get; set; }
        public string Typeofcases { get; set; }
    }
}
