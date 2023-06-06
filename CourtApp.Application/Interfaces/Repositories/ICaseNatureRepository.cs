using CourtApp.Domain.Entities.LawyerDiary;
using CourtApp.Domain.Entities.LawyerDiary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.Interfaces.Repositories
{
    public interface ICaseNatureRepository
    {
        IQueryable<CaseNatureEntity> CaseNatures { get; }

        Task<List<CaseNatureEntity>> GetListAsync();

        Task<CaseNatureEntity> GetByIdAsync(int caseNatureId);

        Task<int> InsertAsync(CaseNatureEntity caseNature);

        Task UpdateAsync(CaseNatureEntity caseNature);

        Task DeleteAsync(CaseNatureEntity caseNature);
    }
}
