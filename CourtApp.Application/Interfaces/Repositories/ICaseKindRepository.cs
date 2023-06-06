using CourtApp.Domain.Entities.LawyerDiary;
using CourtApp.Domain.Entities.LawyerDiary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.Interfaces.Repositories
{
    public interface ICaseKindRepository
    {
        IQueryable<CaseKindEntity> QryEntities { get; }

        Task<List<CaseKindEntity>> GetListAsync();

        Task<CaseKindEntity> GetByIdAsync(int Id);

        Task<int> InsertAsync(CaseKindEntity objEntity);

        Task UpdateAsync(CaseKindEntity objEntity);

        Task DeleteAsync(CaseKindEntity objEntity);
    }
}
