using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Infrastructure.Identity.Models
{
    public class AddressInfo
    {
        public int StateId { get; set; }
        public int DistrictId { get; set; }
        public int CityId { get; set; }
        public string StreetAddress { get; set; }
    }
}
