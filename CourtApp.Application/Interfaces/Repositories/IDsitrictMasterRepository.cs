using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourtApp.Entities.Common;

namespace CourtApp.Application.Interfaces.Repositories
{
    public interface IDsitrictMasterRepository
    {
        IQueryable<DistrictEntity> Entities{get;}
        Task<List<DistrictEntity>> GetDistrictListByStateAsync(int StateCode);
        DistrictEntity GetDistrictById(int Id);
    }
}