using System;
namespace CourtApp.Infrastructure.CacheKeys
{
    internal class CourtComplexCacheKeys
    {
        public static string ListKey => "CourtComplexList";
        public static string GetByIdKey(Guid Id) => $"CourtComplex-{Id}";
    }
}
