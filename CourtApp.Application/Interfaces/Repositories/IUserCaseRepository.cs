using CourtApp.Domain.Entities.LawyerDiary;
using CourtApp.Domain.Entities.LawyerDiary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.Interfaces.Repositories
{
   public interface IUserCaseRepository
    {
        IQueryable<CaseEntity> Entites { get; }
        Task<List<CaseEntity>> GetListAsync();
        Task<CaseEntity> GetByIdAsync(Guid Id);
        Task<Guid> InsertAsync(CaseEntity caseEntity);
        Task UpdateAsync(CaseEntity caseEntity);
        Task DeleteAsync(CaseEntity caseEntity);
    }
}
