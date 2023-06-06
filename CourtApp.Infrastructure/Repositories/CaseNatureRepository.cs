using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.LawyerDiary;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Infrastructure.Repositories
{
    public class CaseNatureRepository : ICaseNatureRepository
    {
        private readonly IRepositoryAsync<CaseNatureEntity> _repository;
        private readonly IDistributedCache _distributedCache;
        public CaseNatureRepository(IRepositoryAsync<CaseNatureEntity> _repository, IDistributedCache _distributedCache)
        {
            this._distributedCache = _distributedCache;
            this._repository = _repository;
        }
        public IQueryable<CaseNatureEntity> CaseNatures => _repository.Entities;

        public async Task DeleteAsync(CaseNatureEntity caseNature)
        {
            await _repository.DeleteAsync(caseNature);
            await _distributedCache.RemoveAsync(CacheKeys.CaseNatureCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(CacheKeys.CaseNatureCacheKeys.GetKey(caseNature.Id));
        }

        public async Task<CaseNatureEntity> GetByIdAsync(int caseNatureId)
        {
            return await _repository.Entities.Where(c => c.Id == caseNatureId).FirstOrDefaultAsync();
        }

        public async Task<List<CaseNatureEntity>> GetListAsync()
        {
            return await _repository.Entities.ToListAsync();
        }

        public async Task<int> InsertAsync(CaseNatureEntity caseNature)
        {
            await _repository.AddAsync(caseNature);
            await _distributedCache.RemoveAsync(CacheKeys.CaseNatureCacheKeys.ListKey);
            return caseNature.Id;
        }

        public async Task UpdateAsync(CaseNatureEntity caseNature)
        {
            await _repository.UpdateAsync(caseNature);
            await _distributedCache.RemoveAsync(CacheKeys.CaseNatureCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(CacheKeys.CaseNatureCacheKeys.GetKey(caseNature.Id));
        }
    }
}
