using CourtApp.Domain.Entities.LawyerDiary;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CourtApp.Application.Interfaces.CacheRepositories
{
    public interface ITypeOfCasesCacheRepository
    {
        Task<List<TypeOfCasesEntity>> GetCachedListAsync();

        Task<TypeOfCasesEntity> GetByIdAsync(Guid Id);
    }
}
