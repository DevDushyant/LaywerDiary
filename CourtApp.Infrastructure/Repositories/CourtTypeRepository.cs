using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.LawyerDiary;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CourtApp.Application.Constants.Permissions;

namespace CourtApp.Infrastructure.Repositories
{
    public class CourtTypeRepository:ICourtTypeRepository
    {
        private readonly IRepositoryAsync<CourtTypeEntity> _repository;
        private readonly IDistributedCache _distributedCache;

        public CourtTypeRepository(IRepositoryAsync<CourtTypeEntity> _repository, IDistributedCache _distributedCache)
        {
            this._repository=_repository;
            this._distributedCache = _distributedCache;
        }

        public IQueryable<CourtTypeEntity> CourtTypeEntities => _repository.Entities;

        public async Task DeleteAsync(CourtTypeEntity courtTypeEntity)
        {
            await _repository.DeleteAsync(courtTypeEntity);
            await _distributedCache.RemoveAsync(CacheKeys.CourtTypeCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(CacheKeys.CourtTypeCacheKeys.GetKey(courtTypeEntity.Id));
        }

        public async Task<CourtTypeEntity> GetByIdAsync(int CourtTypeId)
        {
            return await _repository.Entities.Where(p => p.Id == CourtTypeId).FirstOrDefaultAsync();
        }

        public  async Task<List<CourtTypeEntity>> GetListAsync()
        {
            
            return await _repository.Entities.ToListAsync();
        }

        public async Task<int> InsertAsync(CourtTypeEntity courtTypeEntity)
        {
            await _repository.AddAsync(courtTypeEntity);
            await _distributedCache.RemoveAsync(CacheKeys.CourtTypeCacheKeys.ListKey);
            return courtTypeEntity.Id;
        }

        public async Task UpdateAsync(CourtTypeEntity courtTypeEntity)
        {
            await _repository.UpdateAsync(courtTypeEntity);
            await _distributedCache.RemoveAsync(CacheKeys.CourtTypeCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(CacheKeys.CourtTypeCacheKeys.GetKey(courtTypeEntity.Id));
        }
    }
}
