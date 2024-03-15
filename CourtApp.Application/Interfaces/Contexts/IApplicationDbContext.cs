using CourtApp.Domain.Entities.LawyerDiary;
using CourtApp.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Interfaces.Contexts
{
    public interface IApplicationDbContext
    {
        IDbConnection Connection { get; }
        bool HasChanges { get; }

        EntityEntry Entry(object entity);

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);        
        DbSet<StateEntity> States { get; set; }
        DbSet<DistrictEntity> Districts { get; set; }
        DbSet<BookTypeEntity> BookTypes { get; set; }
        DbSet<LDBookEntity> LDBooks { get; set; }
        DbSet<PublisherEntity> Publishers { get; set; }
        DbSet<ClientEntity> Clients { get; set; }
        DbSet<CourtTypeEntity> CourtType { get; set; }
        DbSet<TypeOfCasesEntity> Typeofcases { get; set; }
        DbSet<CaseKindEntity> CaseKinds { get; set; }
        DbSet<CaseEntity> Cases { get; set; }
        DbSet<LawyerMasterEntity> Laywers { get; set; }
        public DbSet<ProceedingHeadEntity> ProceedingHeads { get; set; }

    }
}