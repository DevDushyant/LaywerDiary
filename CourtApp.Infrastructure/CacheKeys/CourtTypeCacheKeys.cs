using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Infrastructure.CacheKeys
{
    public class CourtTypeCacheKeys
    {
        public static string ListKey => "CourtTypeList";

        public static string SelectListKey => "CourtTypeSelectList";

        public static string GetKey(Guid Id) => $"CourtType-{Id}";

        public static string GetDetailsKey(Guid Id) => $"CourtTypeDetails-{Id}";
    }
}
