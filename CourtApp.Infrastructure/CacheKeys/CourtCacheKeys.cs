using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Infrastructure.CacheKeys
{
    public class CourtCacheKeys
    {
        public static string ListKey => "CourtList";

        public static string SelectListKey => "CourtSelectList";

        public static string GetKey(Guid Id) => $"Court-{Id}";

        public static string GetDetailsKey(Guid Id) => $"CourtDetails-{Id}";
    }
}
