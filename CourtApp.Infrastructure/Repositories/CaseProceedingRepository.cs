using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.LawyerDiary;
using CourtApp.Infrastructure.CacheKeys;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourtApp.Infrastructure.Repositories
{
    public class CaseProceedingRepository : ICaseProceedingRepository
    {
        private readonly IRepositoryAsync<CaseProcedingEntity> _repository;
        private readonly IDistributedCache _distributedCache;
        public CaseProceedingRepository(IRepositoryAsync<CaseProcedingEntity> _repository,
            IDistributedCache _distributedCache)
        {
            this._repository = _repository;
            this._distributedCache = _distributedCache;
        }
        public IQueryable<CaseProcedingEntity> Entities => _repository.Entities;

        public async Task<Guid> AddAsync(CaseProcedingEntity Entity)
        {
            await _repository.AddAsync(Entity);
            await _distributedCache.RemoveAsync(AppCacheKeys.ProcHeadKey);
            return Entity.Id;
        }

        public Task DeleteAsync(CaseProcedingEntity Entity)
        {
            throw new NotImplementedException();
        }

        public Task<CaseProcedingEntity> GetByIdAsync(Guid CaseId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<CaseProcedingEntity>> GetListAsync()
        {
            var data = await _repository.Entities
                .Include(w => w.SubHead)
                .ToListAsync();
            return data;
        }

        public async Task<List<CaseProcedingEntity>> GetProceedingByCaseIdAsync(Guid CaseId)
        {
            var data = await _repository.Entities
                .Include(w => w.Head)
                .Include(w => w.SubHead)
                .Include(w => w.Stage)
                .ToListAsync();
            return data;
        }

        public async Task<Guid> InsertAsync(List<CaseProcedingEntity> Entity)
        {
            await _repository.BulkInsert(Entity);
            await _distributedCache.RemoveAsync(AppCacheKeys.CourtComplexKey);
            return Entity.Select(s => s.CaseId).FirstOrDefault();
        }

        public Task UpdateAsync(List<CaseProcedingEntity> Entity)
        {
            throw new NotImplementedException();
        }
    }
}
