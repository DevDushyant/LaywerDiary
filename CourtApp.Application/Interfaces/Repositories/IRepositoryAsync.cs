﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourtApp.Application.Interfaces.Repositories
{
    public interface IRepositoryAsync<T> where T : class
    {
        IQueryable<T> Entities { get; }
        Task<T> GetByIdAsync(int id);
        Task<List<T>> GetAllAsync();
        Task<List<T>> GetPagedReponseAsync(int pageNumber, int pageSize);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task AddRange(List<T> entity);
        Task UpdateRangeAsync(List<T> entities);
        Task DeleteRangeAsync(List<T> entities);
    }
}