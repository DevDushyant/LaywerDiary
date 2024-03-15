using CourtApp.Domain.Entities.LawyerDiary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.Interfaces.Repositories
{
    public interface ICourtTypeRepository
    {
        IQueryable<CourtTypeEntity> CourtTypeEntities { get; }

        Task<List<CourtTypeEntity>> GetListAsync();

        Task<CourtTypeEntity> GetByIdAsync(Guid CourtTypeId);

        Task<Guid> InsertAsync(CourtTypeEntity courtTypeEntity);

        Task UpdateAsync(CourtTypeEntity courtTypeEntity);

        Task DeleteAsync(CourtTypeEntity courtTypeEntity);
    }
}
