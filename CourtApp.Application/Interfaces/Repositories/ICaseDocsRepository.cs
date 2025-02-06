using CourtApp.Domain.Entities.LawyerDiary;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CourtApp.Application.Interfaces.Repositories
{
    public interface ICaseDocsRepository
    {
        Task<Guid> SaveCaseDocAsync(CaseDocsEntity entity);
        IQueryable<CaseDocsEntity> Entities { get; }
        Task DeleteAsync(CaseDocsEntity objEntity);

    }
}
