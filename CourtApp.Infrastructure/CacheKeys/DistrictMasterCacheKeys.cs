using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourtApp.Infrastructure.CacheKeys
{
    public class DistrictMasterCacheKeys
    {
        public static string SelectListByStateKey(string StateCode) => $"DistrictSelectList-{StateCode}";
         
    }
}