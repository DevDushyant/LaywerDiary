using CourtApp.Domain.Entities.LawyerDiary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.Interfaces.Repositories
{
    public interface ISubjectRepository
    {
        IQueryable<PracticeSubjectEntity> Subjects { get; }

        Task<List<PracticeSubjectEntity>> GetListAsync();

        Task<PracticeSubjectEntity> GetByIdAsync(int Id);

        Task<int> InsertAsync(PracticeSubjectEntity subject);

        Task UpdateAsync(PracticeSubjectEntity subject);

        Task DeleteAsync(PracticeSubjectEntity subject);
    }
}
