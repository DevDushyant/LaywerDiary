using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Infrastructure.CacheKeys
{
    public class TypeOfCasesCacheKeys
    {
        public static string ListKey => "TypeofcasesList";

        public static string SelectListKey => "TypeofcasesSelectList";

        public static string GetKey(int Id) => $"Typeofcases-{Id}";

        public static string GetDetailsKey(int Id) => $"TypeofcasesDetails-{Id}";
    }
}
