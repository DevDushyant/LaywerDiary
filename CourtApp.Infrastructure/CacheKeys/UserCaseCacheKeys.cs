using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Infrastructure.CacheKeys
{
    public class UserCaseCacheKeys
    {
        public static string ListKey => "UserList";

        public static string SelectListKey => "UserSelectList";

        public static string GetKey(Guid CaseUid) => $"User-{CaseUid}";

        public static string GetDetailsKey(Guid CaseUid) => $"UserDetails-{CaseUid}";
    }
}
