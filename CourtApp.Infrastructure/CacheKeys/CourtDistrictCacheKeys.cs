using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Infrastructure.CacheKeys
{
    public class CourtDistrictCacheKeys
    {
        public static string ListKey => "CourtDistrictList";        
        public static string GetByIdKey(Guid Id) => $"CourtDistrict-{Id}";        
    }
}
