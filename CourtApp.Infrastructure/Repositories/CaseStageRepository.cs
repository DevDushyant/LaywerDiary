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
    public class CaseStageRepository : ICaseStageRepository
    {
        private readonly IRepositoryAsync<CaseStageEntity> _repository;
        private readonly IDistributedCache _distributedCache;
        public CaseStageRepository(IRepositoryAsync<CaseStageEntity> _repository, IDistributedCache _distributedCache)
        {
            this._repository = _repository;
            this._distributedCache = _distributedCache;
        }
        public IQueryable<CaseStageEntity> QryEntities => _repository.Entities;

        public async Task DeleteAsync(CaseStageEntity objEntity)
        {
            await _repository.DeleteAsync(objEntity);
            await _distributedCache.RemoveAsync(CacheKeys.CaseStageCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(CacheKeys.CaseStageCacheKeys.GetKey(objEntity.Id));
        }

        public async Task<CaseStageEntity> GetByIdAsync(int Id)
        {
            return await _repository.Entities.Where(p => p.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<List<CaseStageEntity>> GetListAsync()
        {
            return await _repository.Entities.OrderByDescending(o => o.CaseStage).ToListAsync();
        }

        public async Task<int> InsertAsync(CaseStageEntity objEntity)
        {
            await _repository.AddAsync(objEntity);
            await _distributedCache.RemoveAsync(CacheKeys.CaseStageCacheKeys.ListKey);
            return objEntity.Id;
        }

        public async Task UpdateAsync(CaseStageEntity objEntity)
        {
            await _repository.UpdateAsync(objEntity);
            await _distributedCache.RemoveAsync(CacheKeys.CaseStageCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(CacheKeys.CaseStageCacheKeys.GetKey(objEntity.Id));
        }
    }
}
