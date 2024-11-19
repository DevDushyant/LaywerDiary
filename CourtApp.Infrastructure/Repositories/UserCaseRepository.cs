﻿using CourtApp.Application.Interfaces.Repositories;
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
                .Where(w => w.CaseNo.Equals(No) && w.CaseYear == Year).FirstOrDefaultAsync();
        }

        public async Task<CaseDetailEntity> GetByIdAsync(Guid Id)
        {
            return await _repository
               .Entities
                .Include(d => d.CaseAgainstEntities)
                    .ThenInclude(c => c.CourtType)
                .Include(d => d.CourtType)
                .Include(c => c.CourtBench)                
                .Where(w => w.Id == Id).FirstAsync();
        }

        public async Task<CaseDetailEntity> GetDetailAsync(Guid CaseUid)
        {
            return await _repository
                .Entities
                .Include(c => c.State)
                .Include(c => c.CaseCategory)
                .Include(c => c.CaseStage)
                .Include(c => c.CourtType)
                .Include(c => c.CourtBench)
                .Include(c => c.CaseType)
                .Include(t => t.FTitle)
                .Include(t => t.STitle)
                .Include(t => t.CourtDistrict)
                .Include(e => e.Complex)
                .Include(d => d.CaseAgainstEntities).ThenInclude(c => c.CourtBench)
                .Include(d => d.CaseAgainstEntities).ThenInclude(c => c.CourtType)
                .Include(d => d.CaseAgainstEntities).ThenInclude(c => c.CaseCategory)
                .Include(d => d.CaseAgainstEntities).ThenInclude(c => c.Complex)
                .Include(d => d.CaseAgainstEntities).ThenInclude(c => c.CaseType)
                .Include(d => d.CaseAgainstEntities).ThenInclude(c => c.CourtDistrict)
                .Where(w => w.Id == CaseUid).FirstAsync();
        }

        public async Task<List<CaseDetailEntity>> GetListAsync()
        {
            return await _repository.Entities.ToListAsync();
        }

        public async Task<CaseDetailEntity> GetMostRecentCaseInfo(string UserId)
        {
            var IsRecord = _repository.Entities.Where(w => w.CreatedBy.Equals(UserId));
            if (IsRecord.Count() > 0)
            {
                return await _repository
                   .Entities
                    .Include(d => d.CaseAgainstEntities)
                        .ThenInclude(c => c.CourtType)
                    .Include(d => d.CourtType)
                    .Include(c => c.CourtBench)
                    .Where(u => u.CreatedBy.Equals(UserId))
                    .OrderByDescending(u => u.CreatedOn)
                    .FirstAsync();
            }
            return null;
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
