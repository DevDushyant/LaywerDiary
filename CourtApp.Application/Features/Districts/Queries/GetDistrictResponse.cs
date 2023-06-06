using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.Districts.Queries
{
    public class GetDistrictResponse
    {
        public int DistrictCode { get; set; }
        public string DistrictName { get; set; }
    }
}