using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;

namespace CourtApp.Infrastructure.Repositories
{
    public class DistrictMasterRepository:IDsitrictMasterRepository
    {
         private readonly IDistributedCache _distributedCache;
          private readonly IRepositoryAsync<DistrictEntity> _repository;
          public DistrictMasterRepository(IDistributedCache _distributedCache,IRepositoryAsync<DistrictEntity> _repository)
          {
            this._distributedCache=_distributedCache;
            this._repository=_repository;
          }

        public IQueryable<DistrictEntity> Entities => _repository.Entities;

        public async Task<List<DistrictEntity>> GetDistrictListByStateAsync(string StateCode)
        {
            return await _repository.Entities.Where(st=>st.StateCode.ToLower().Equals(StateCode.ToLower())).ToListAsync();
        }
    }
}