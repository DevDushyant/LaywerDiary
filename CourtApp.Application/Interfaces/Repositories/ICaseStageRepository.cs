using CourtApp.Domain.Entities.LawyerDiary;
using CourtApp.Domain.Entities.LawyerDiary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.Interfaces.Repositories
{
    public interface ICaseStageRepository
    {
        IQueryable<CaseStageEntity> QryEntities { get; }

        Task<List<CaseStageEntity>> GetListAsync();

        Task<CaseStageEntity> GetByIdAsync(int Id);

        Task<int> InsertAsync(CaseStageEntity objEntity);

        Task UpdateAsync(CaseStageEntity objEntity);

        Task DeleteAsync(CaseStageEntity objEntity);
    }
}
