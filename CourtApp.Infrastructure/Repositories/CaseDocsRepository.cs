using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.LawyerDiary;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CourtApp.Infrastructure.Repositories
{
    public class CaseDocsRepository : ICaseDocsRepository
    {
        private readonly IRepositoryAsync<CaseDocsEntity> _repository;
        public CaseDocsRepository(IRepositoryAsync<CaseDocsEntity> _repository)
        {
            this._repository = _repository;
        }

        public IQueryable<CaseDocsEntity> Entities => _repository.Entities;

        public async Task DeleteAsync(CaseDocsEntity objEntity)
        {
            await _repository.DeleteAsync(objEntity);
        }

        public async Task<Guid> SaveCaseDocAsync(CaseDocsEntity entity)
        {
            await _repository.AddAsync(entity);
            return entity.Id;
        }
        public async Task DeleteAsync(CaseDocsEntity objEntity)
        {
            await _repository.DeleteAsync(objEntity);
        }
    }
}
