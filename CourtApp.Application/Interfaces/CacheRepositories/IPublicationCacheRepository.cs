using CourtApp.Domain.Entities.LawyerDiary;
using CourtApp.Domain.Entities.LawyerDiary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.Interfaces.CacheRepositories
{
    public interface IPublicationCacheRepository
    {
        Task<List<PublisherEntity>> GetCachedListAsync();

        Task<PublisherEntity> GetByIdAsync(Guid bookTypeId);
    }
}
