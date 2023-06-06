using CourtApp.Application.Interfaces.CacheRepositories;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Infrastructure.CacheKeys;
using AspNetCoreHero.Extensions.Caching;
using AspNetCoreHero.ThrowR;
using Microsoft.Extensions.Caching.Distributed;
using System.Collections.Generic;
using System.Threading.Tasks;
using CourtApp.Domain.Entities.LawyerDiary;

namespace CourtApp.Infrastructure.CacheRepositories
{
    public class SubjectCacheRepository : ISubjectCacheRepository
    {
        private readonly IDistributedCache _distributedCache;
        private readonly ISubjectRepository _repository;

        public SubjectCacheRepository(IDistributedCache distributedCache, ISubjectRepository _repository)
        {
            _distributedCache = distributedCache;
            this._repository = _repository;
        }
        public async Task<PracticeSubjectEntity> GetByIdAsync(int Id)
        {
            string cacheKey = SubjectCacheKeys.GetKey(Id);
            var subjectlist = await _distributedCache.GetAsync<PracticeSubjectEntity>(cacheKey);
            if (subjectlist == null)
            {
                subjectlist = await _repository.GetByIdAsync(Id);
                Throw.Exception.IfNull(subjectlist, "PracticeSubject", "No Subject  Found");
                await _distributedCache.SetAsync(cacheKey, subjectlist);
            }
            return subjectlist;
        }

        public async Task<List<PracticeSubjectEntity>> GetCachedListAsync()
        {
            string cacheKey = SubjectCacheKeys.ListKey;
            var subjectlist = await _distributedCache.GetAsync<List<PracticeSubjectEntity>>(cacheKey);
            if (subjectlist == null)
            {
                subjectlist = await _repository.GetListAsync();
                await _distributedCache.SetAsync(cacheKey, subjectlist);
            }
            return subjectlist;
        }
    }
}
