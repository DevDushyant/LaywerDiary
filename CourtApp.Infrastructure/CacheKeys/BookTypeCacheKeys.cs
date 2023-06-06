using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Infrastructure.CacheKeys
{
    public class BookTypeCacheKeys
    {
        public static string ListKey => "BookTypeList";

        public static string SelectListKey => "BookTypeSelectList";

        public static string GetKey(int brandId) => $"BookType-{brandId}";

        public static string GetDetailsKey(int brandId) => $"BookTypeDetails-{brandId}";
    }
}
