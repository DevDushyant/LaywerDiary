﻿using CourtApp.Domain.Entities.CaseDetails;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CourtApp.Application.Interfaces.Repositories
{
    public interface ICaseAssignedRepository
    {
        IQueryable<AssignCaseEntity> Entities { get; }
        Task<Guid> InsertAsync(AssignCaseEntity entity);
        Task UpdateAsync(AssignCaseEntity entity);
        Task DeleteAsync(AssignCaseEntity entity);
    }
}
