using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CourtMasters.Query
{
    public class GetCourtMasterDataAllResponse
    {
        public int UniqueId { get; set; }
        public string CourtType { get; set; }
        public string CourtName { get; set; }
        public string CourtFullName { get; set; }
        public string Bench { get; set; }
        public string HeadQuerter { get; set; }
        public string Address { get; set; }
        public string State { get; set; }
        public string District { get; set; }
    }
}
