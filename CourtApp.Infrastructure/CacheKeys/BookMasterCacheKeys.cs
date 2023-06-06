using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Infrastructure.CacheKeys
{
    public class BookMasterCacheKeys
    {
        public static string ListKey => "BookMasterList";

        public static string SelectListKey => "BookMasterSelectList";

        public static string GetKey(int brandId) => $"BookMaster-{brandId}";

        public static string GetDetailsKey(int brandId) => $"BookMasterDetails-{brandId}";
    }
}
