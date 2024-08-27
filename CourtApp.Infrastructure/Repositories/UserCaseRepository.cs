using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.CaseDetails;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourtApp.Infrastructure.Repositories
{
    public class UserCaseRepository : IUserCaseRepository
    {
        private readonly IRepositoryAsync<CaseDetailEntity> _repository;
        private readonly IDistributedCache _distributedCache;
        public UserCaseRepository(IRepositoryAsync<CaseDetailEntity> _repository, IDistributedCache _distributedCache)
        {
            this._distributedCache = _distributedCache;
            this._repository = _repository;
        }
        public IQueryable<CaseDetailEntity> Entites => _repository.Entities;

        public async Task DeleteAsync(CaseDetailEntity objEntity)
        {
            await _repository.DeleteAsync(objEntity);
            await _distributedCache.RemoveAsync(CacheKeys.UserCaseCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(CacheKeys.UserCaseCacheKeys.GetKey(objEntity.Id));
        }

        public async Task<CaseDetailEntity> GetByCaseNoYearAsync(string No, int Year)
        {
            return await _repository
                .Entities
                .Where(w => w.CaseNo.Equals(No) && w.CaseYear==Year).FirstOrDefaultAsync();
        }

        public async Task<CaseDetailEntity> GetByIdAsync(Guid CaseUid)
        {
            return await _repository
                .Entities     
                .Include(c=>c.State)
                .Include(c=>c.CaseCategory)
                .Include(c=>c.CaseStage)
                .Include(c=>c.CourtType)
                .Include(c=>c.CourtBench)
                .Include(c=>c.CaseType)
                .Where(w=>w.Id==CaseUid).FirstAsync();            
        }

        public async Task<List<CaseDetailEntity>> GetListAsync()
        {
            return await _repository.Entities.ToListAsync();
        }

        public async Task<Guid> InsertAsync(CaseDetailEntity objEntity)
        {
            await _repository.AddAsync(objEntity);
            await _distributedCache.RemoveAsync(CacheKeys.UserCaseCacheKeys.ListKey);
            return objEntity.Id;
        }

        public async Task UpdateAsync(CaseDetailEntity objEntity)
        {
            await _repository.UpdateAsync(objEntity);
            await _distributedCache.RemoveAsync(CacheKeys.UserCaseCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(CacheKeys.UserCaseCacheKeys.GetKey(objEntity.Id));
        }
    }
}
