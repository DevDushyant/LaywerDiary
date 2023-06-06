using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreHero.Extensions.Caching;
using CourtApp.Application.Interfaces.CacheRepositories;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Entities.Common;
using CourtApp.Infrastructure.CacheKeys;
using Microsoft.Extensions.Caching.Distributed;

namespace CourtApp.Infrastructure.CacheRepositories
{
    public class DistrictMasterCacheRepository:IDsitrictMasterCacheRepository
    {
        private readonly IDsitrictMasterRepository _repository;
        private readonly IDistributedCache _distributedCache;
        public DistrictMasterCacheRepository(IDsitrictMasterRepository _repository, IDistributedCache _distributedCache)
        {
            this._distributedCache = _distributedCache;
            this._repository = _repository;
        }

        public async Task<List<DistrictEntity>> GetDistrictListByStateAsync(string StateCode)
        {
            string cacheKey = DistrictMasterCacheKeys.SelectListByStateKey(StateCode);
            var DistrictList = await _distributedCache.GetAsync<List<DistrictEntity>>(cacheKey);
            if (DistrictList==null)
            {
                DistrictList = await _repository.GetDistrictListByStateAsync(StateCode);
                await _distributedCache.SetAsync(cacheKey, DistrictList);
            }
            return DistrictList;
        }
    }
}