using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.LawyerDiary;

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
        private readonly IRepositoryAsync<CaseEntity> _repository;
        private readonly IDistributedCache _distributedCache;
        public UserCaseRepository(IRepositoryAsync<CaseEntity> _repository, IDistributedCache _distributedCache)
        {
            this._distributedCache = _distributedCache;
            this._repository = _repository;
        }
        public IQueryable<CaseEntity> Entites => _repository.Entities;

        public async Task DeleteAsync(CaseEntity objEntity)
        {
            await _repository.DeleteAsync(objEntity);
            await _distributedCache.RemoveAsync(CacheKeys.UserCaseCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(CacheKeys.UserCaseCacheKeys.GetKey(objEntity.Id));
        }

        public async Task<CaseEntity> GetByIdAsync(Guid CaseUid)
        {
            return await _repository.Entities.Where(p => p.Id == CaseUid).FirstOrDefaultAsync();
        }

        public async Task<List<CaseEntity>> GetListAsync()
        {
            return await _repository.Entities.ToListAsync();
        }

        public async Task<Guid> InsertAsync(CaseEntity objEntity)
        {
            await _repository.AddAsync(objEntity);
            await _distributedCache.RemoveAsync(CacheKeys.UserCaseCacheKeys.ListKey);
            return objEntity.Id;
        }

        public async Task UpdateAsync(CaseEntity objEntity)
        {
            await _repository.UpdateAsync(objEntity);
            await _distributedCache.RemoveAsync(CacheKeys.UserCaseCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(CacheKeys.UserCaseCacheKeys.GetKey(objEntity.Id));
        }
    }
}
