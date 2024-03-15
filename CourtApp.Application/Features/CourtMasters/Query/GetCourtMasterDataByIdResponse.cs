using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CourtMasters.Query
{
    public class GetCourtMasterDataByIdResponse
    {
        public Guid CourtTypeId { get; set; }
        public int DistrictCode { get; set; }
        public int StateCode { get; set; }
        public string CourtName { get; set; }
        public string Bench { get; set; }
        public string HeadQuerter { get; set; }
        public string Address { get; set; }
    }
}
