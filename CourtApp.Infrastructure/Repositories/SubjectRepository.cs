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
    public class SubjectRepository : ISubjectRepository
    {
        private readonly IRepositoryAsync<PracticeSubjectEntity> _repository;
        private readonly IDistributedCache _distributedCache;

        public SubjectRepository(IRepositoryAsync<PracticeSubjectEntity> _repository, IDistributedCache _distributedCache)
        {
            this._repository = _repository;
            this._distributedCache = _distributedCache;
        }
        public IQueryable<PracticeSubjectEntity> Subjects => _repository.Entities;

        public async Task DeleteAsync(PracticeSubjectEntity subject)
        {
            await _repository.DeleteAsync(subject);
            await _distributedCache.RemoveAsync(CacheKeys.SubjectCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(CacheKeys.SubjectCacheKeys.GetKey(subject.Id));
        }

        public async Task<PracticeSubjectEntity> GetByIdAsync(int Id)
        {
            return await _repository.Entities.Where(p => p.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<List<PracticeSubjectEntity>> GetListAsync()
        {
            return await _repository.Entities.OrderByDescending(o => o.Subject).ToListAsync();
        }

        public async Task<int> InsertAsync(PracticeSubjectEntity subject)
        {
            await _repository.AddAsync(subject);
            await _distributedCache.RemoveAsync(CacheKeys.SubjectCacheKeys.ListKey);
            return subject.Id;
        }

        public async Task UpdateAsync(PracticeSubjectEntity subject)
        {
            await _repository.UpdateAsync(subject);
            await _distributedCache.RemoveAsync(CacheKeys.SubjectCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(CacheKeys.SubjectCacheKeys.GetKey(subject.Id));
        }
    }
}
