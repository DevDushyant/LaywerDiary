using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Infrastructure.CacheKeys
{
   public class SubjectCacheKeys
    {
        public static string ListKey => "SubjectList";

        public static string SelectListKey => "SubjectSelectList";

        public static string GetKey(Guid brandId) => $"Subject-{brandId}";

        public static string GetDetailsKey(Guid brandId) => $"SubjectDetails-{brandId}";
    }
}
