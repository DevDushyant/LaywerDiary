﻿using CourtApp.Domain.Entities.LawyerDiary;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CourtApp.Application.Interfaces.CacheRepositories
{
    public interface IBookMasterCacheRepository
    {
        Task<List<LDBookEntity>> GetCachedListAsync();      

        Task<LDBookEntity> GetByIdAsync(int bookTypeId);
    }
}
