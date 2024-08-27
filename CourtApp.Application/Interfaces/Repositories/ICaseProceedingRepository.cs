using CourtApp.Domain.Entities.CaseDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.Interfaces.Repositories
{
    public interface ICaseProceedingRepository
    {
        IQueryable<CaseProcedingEntity> Entities { get; }
        Task<List<CaseProcedingEntity>> GetListAsync();
        Task<List<CaseProcedingEntity>> GetProceedingByCaseIdAsync(Guid CaseId);
        Task<CaseProcedingEntity> GetByIdAsync(Guid CaseId);
        Task<Guid> AddAsync(CaseProcedingEntity Entity);
        Task UpdateAsync(List<CaseProcedingEntity> Entity);
        Task DeleteAsync(CaseProcedingEntity Entity);
    }
}
