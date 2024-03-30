using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.LawyerDiary;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Infrastructure.Repositories
{
    public class CaseManagmentRepository: ICaseManagmentRepository
    {
        private readonly IRepositoryAsync<CaseEntity> _repository;
        private readonly IDistributedCache _distributedCache;
        public CaseManagmentRepository(IRepositoryAsync<CaseEntity> _repository, IDistributedCache _distributedCache)
        {
            this._repository = _repository;
            this._distributedCache = _distributedCache;
        }
        public IQueryable<CaseEntity> Entities => _repository.Entities;
        public async Task<List<CaseEntity>> GetListAsync()
        {
            try { var data = _repository.Entities.ToListAsync(); return await data; }
           catch (Exception ex)
            { return null; }           


        }
        public async Task<CaseEntity> GetByIdAsync(Guid Id)
        {
            var DetailDt = await _repository.Entities
                .Where(c => c.Id == Id).FirstOrDefaultAsync();
            return DetailDt;
        }
        public async Task<Guid> InsertAsync(CaseEntity workMasterEntity)
        {
            //foreach (var item in workMasterEntity.AgainstCaseDetails)
            //{
            //    item. = workMasterEntity.Id;
            //}
            //await _repository.AddAsync(workMasterEntity);
            return workMasterEntity.Id;
        }

        public async Task UpdateAsync(CaseEntity workMasterEntity)
        {
            await _repository.UpdateAsync(workMasterEntity);
        }
        public async Task DeleteAsync(CaseEntity workMasterEntity)
        {
            await _repository.DeleteAsync(workMasterEntity);
        }

    }
}
