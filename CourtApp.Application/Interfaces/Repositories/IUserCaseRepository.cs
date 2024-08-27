using CourtApp.Domain.Entities.CaseDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.Interfaces.Repositories
{
    public interface IUserCaseRepository
    {
        IQueryable<CaseDetailEntity> Entites { get; }
        Task<List<CaseDetailEntity>> GetListAsync();
        Task<CaseDetailEntity> GetByIdAsync(Guid Id);
        Task<Guid> InsertAsync(CaseDetailEntity caseEntity);
        Task UpdateAsync(CaseDetailEntity caseEntity);
        Task DeleteAsync(CaseDetailEntity caseEntity);
        Task<CaseDetailEntity> GetByCaseNoYearAsync(string No, int Year);
    }
}
