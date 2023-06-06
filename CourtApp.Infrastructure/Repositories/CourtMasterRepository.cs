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
    public class CourtMasterRepository : ICourtMasterRepository
    {
        private readonly IRepositoryAsync<CourtMasterEntity> _repository;
        private readonly IDistributedCache _distributedCache;

        public CourtMasterRepository(IRepositoryAsync<CourtMasterEntity> _repository, IDistributedCache _distributedCache)
        {
            this._repository = _repository;
            this._distributedCache = _distributedCache;
        }

        public IQueryable<CourtMasterEntity> QryEntities => _repository.Entities;

        public Task DeleteAsync(CourtMasterEntity objEntity)
        {
            throw new NotImplementedException();
        }

        public async Task<CourtMasterEntity> GetByIdAsync(Guid Id)
        {
            return await _repository.Entities.Where(p => p.UniqueId==Id).FirstOrDefaultAsync();
        }

        public async Task<List<CourtMasterEntity>> GetListAsync()
        {
            return await _repository.Entities.OrderByDescending(o => o.CourtName).ToListAsync();
        }

        public async Task<int> InsertAsync(CourtMasterEntity objEntity)
        {
            await _repository.AddAsync(objEntity);
            await _distributedCache.RemoveAsync(CacheKeys.CourtCacheKeys.ListKey);
            return objEntity.Id;
        }

        public async Task UpdateAsync(CourtMasterEntity objEntity)
        {
            await _repository.UpdateAsync(objEntity);
            await _distributedCache.RemoveAsync(CacheKeys.CourtCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(CacheKeys.CourtCacheKeys.GetKey(objEntity.UniqueId));
        }
    }
}
