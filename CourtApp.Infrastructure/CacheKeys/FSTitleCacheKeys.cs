using System;
namespace CourtApp.Infrastructure.CacheKeys
{
    public class FSTitleCacheKeys
    {
        public static string ListKey => "FSTitleList";
        public static string GetKey(Guid Id) => $"FSTitle-{Id}";        
    }
}
