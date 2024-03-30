using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Infrastructure.CacheKeys
{
    public class CaseTitleCacheKeys
    {
        public static string ListKey => "TitleList";        
        public static string GetKey(Guid Id) => $"TitleDetail-{Id}";
    }
}
