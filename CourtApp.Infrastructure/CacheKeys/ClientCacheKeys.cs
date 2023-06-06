using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Infrastructure.CacheKeys
{
    public class ClientCacheKeys
    {
        public static string ListKey => "ClientList";

        public static string SelectListKey => "ClientSelectList";

        public static string GetKey(int clientId) => $"Client-{clientId}";

        public static string GetDetailsKey(int clientId) => $"ClientDetails-{clientId}";
    }
}
