using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CaseKinds.Query
{
    public class CaseKindQueryByIdResponse
    {
        public int Id { get; set; }
        public string CaseKind { get; set; }
        public int CourtTypeId { get; set; }
    }
}
