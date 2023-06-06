using CourtApp.Domain.Entities.LawyerDiary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.Interfaces.Repositories
{
  public  interface IPublicationRepository
    {
        IQueryable<PublisherEntity> BookTypes { get; }

        Task<List<PublisherEntity>> GetListAsync();

        Task<PublisherEntity> GetByIdAsync(int bookTypeId);

        Task<int> InsertAsync(PublisherEntity bookType);

        Task UpdateAsync(PublisherEntity bookType);

        Task DeleteAsync(PublisherEntity bookType);
    }
}
