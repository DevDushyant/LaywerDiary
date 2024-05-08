using CourtApp.Application.Features.Case;
using CourtApp.Domain.Entities.LawyerDiary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.Interfaces.Repositories
{
    public interface ICaseAgainstRepository
    {
        IQueryable<CaseDetailAgainstEntity> Entities { get; }
        Task<List<CaseDetailAgainstEntity>> GetListAsync();
        Task<CaseDetailAgainstEntity> GetByIdAsync(Guid Id);
        Task<Guid> InsertAsync(List<CaseDetailAgainstEntity> Entity);
        Task UpdateAsync(List<CaseDetailAgainstEntity> Entity);
        Task DeleteAsync(CaseDetailAgainstEntity Entity);
    }
}
