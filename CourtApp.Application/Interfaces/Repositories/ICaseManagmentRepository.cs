using CourtApp.Domain.Entities.LawyerDiary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.Interfaces.Repositories
{
    public interface ICaseManagmentRepository
    {
        IQueryable<CaseEntity> Entities { get; }
        Task<List<CaseEntity>> GetListAsync();
        Task<CaseEntity> GetByIdAsync(Guid Id);
        Task<Guid> InsertAsync(CaseEntity workMasterEntity);
        Task UpdateAsync(CaseEntity workMasterEntity);
        Task DeleteAsync(CaseEntity workMasterEntity);
    }
}
