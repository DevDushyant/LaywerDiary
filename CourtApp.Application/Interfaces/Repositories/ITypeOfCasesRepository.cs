using CourtApp.Domain.Entities.LawyerDiary;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourtApp.Application.Interfaces.Repositories
{
    public interface ITypeOfCasesRepository
    {
        IQueryable<TypeOfCasesEntity> QryEntities { get; }

        Task<List<TypeOfCasesEntity>> GetListAsync();

        Task<TypeOfCasesEntity> GetByIdAsync(int Id);

        Task<int> InsertAsync(TypeOfCasesEntity objEntity);

        Task UpdateAsync(TypeOfCasesEntity objEntity);

        Task DeleteAsync(TypeOfCasesEntity objEntity);
    }
}
