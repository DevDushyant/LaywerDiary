using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Infrastructure.CacheKeys
{
    internal class FormTempMappingCacheKeys
    {
        public static string ListKey => "TempFormList";        
        public static string GetKey(Guid Id) => $"TempForm-{Id}";
        public static string GetDetailsKey(Guid Id) => $"TempFormDetails-{Id}";
    }
}
