using CourtApp.Domain.Entities.LawyerDiary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.Interfaces.Repositories
{
    public interface ICaseProcedingMasterRepository
    {
        IQueryable<CaseProcedingMasterEntity> QryEntities { get; }

        Task<List<CaseProcedingMasterEntity>> GetListAsync();

        Task<CaseProcedingMasterEntity> GetByIdAsync(int Id);

        Task<int> InsertAsync(CaseProcedingMasterEntity objEntity);

        Task UpdateAsync(CaseProcedingMasterEntity objEntity);

        Task DeleteAsync(CaseProcedingMasterEntity objEntity);
    }
}
