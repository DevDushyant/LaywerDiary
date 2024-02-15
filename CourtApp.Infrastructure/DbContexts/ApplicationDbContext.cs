//using AspNetCoreHero.Abstractions.Domain;
using CourtApp.Application.Interfaces.Contexts;
using CourtApp.Application.Interfaces.Shared;
//using AspNetCoreHero.EntityFrameworkCore.AuditTrail;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CourtApp.Entities.Common;
using CourtApp.Domain.Entities.LawyerDiary;
using CourtApp.Domain.Entities.Common;
using AuditTrail.Abstrations;
using System;
namespace CourtApp.Infrastructure.DbContexts
{
    public class ApplicationDbContext : AuditableContext, IApplicationDbContext
    {
        private readonly IDateTimeService _dateTime;
        private readonly IAuthenticatedUserService _authenticatedUser;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options,
            IDateTimeService dateTime,
            IAuthenticatedUserService authenticatedUser) : base(options)
        {
            _dateTime = dateTime;
            _authenticatedUser = authenticatedUser;
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

        public IDbConnection Connection => Database.GetDbConnection();

        public bool HasChanges => ChangeTracker.HasChanges();

       
        public DbSet<StateEntity> States { get; set; }
        public DbSet<DistrictEntity> Districts { get; set; }
        public DbSet<CityEntity> CityEntities { get; set; }
        public DbSet<BookTypeEntity> BookTypes { get; set; }
        public DbSet<CaseNatureEntity> CaseNatureEntities { get; set; }
        public DbSet<LDBookEntity> LDBooks { get; set; }
        public DbSet<PublisherEntity> Publishers { get; set; }
        public DbSet<PracticeSubjectEntity> PracticeSubjects { get; set; }
        public DbSet<ExpenseHeadEntity> ExpenseHeads { get; set; }
        public DbSet<CourtFeeEntity> CourtFees { get; set; }
        public DbSet<CourtFeeTypeEntity> CourtFeeTypes { get; set; }

        public DbSet<CaseKindEntity> CaseKinds { get; set; }
        public DbSet<TypeOfCasesEntity> Typeofcases { get; set; }
        public DbSet<CaseStageEntity> CaseStages { get; set; }
        public DbSet<CourtMasterEntity> CourtMasters { get; set; }
        public DbSet<CourtFeeStructureEntity> CourtFeeStructures { get; set; }
        public DbSet<ClientEntity> Clients { get; set; }
        public DbSet<CourtTypeEntity> CourtType { get; set; }
        public DbSet<CaseEntity> Cases { get; set; }
        public DbSet<LawyerMasterEntity> Laywers { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>().ToList())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedOn = _dateTime.NowUtc;
                        entry.Entity.CreatedBy = _authenticatedUser.UserId;
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastModifiedOn = _dateTime.NowUtc;
                        entry.Entity.LastModifiedBy = _authenticatedUser.UserId;
                        break;


                }
            }
            if (_authenticatedUser.UserId == null)
            {
                return await base.SaveChangesAsync(cancellationToken);
            }
            else
            {
                return await base.SaveChangesAsync(_authenticatedUser.UserId);                
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            foreach (var property in builder.Model.GetEntityTypes()
            .SelectMany(t => t.GetProperties())
            .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
            {
                property.SetColumnType("decimal(18,2)");
            }

            base.OnModelCreating(builder);

            builder.Entity<CourtMasterEntity>().HasIndex(i => i.UniqueId).IsUnique();
            builder.Entity<CourtMasterEntity>().Property(p => p.UniqueId).HasDefaultValueSql("uuid_generate_v4()");
            builder.Entity<CaseEntity>().Property(p => p.Id).HasDefaultValueSql("uuid_generate_v4()");

            
            builder.Entity<StateEntity>().HasKey(c => c.StateCode);
            builder.Entity<DistrictEntity>().HasKey(c => c.DistrictCode);


            #region Filter Data by Logged In User
            builder.Entity<ClientEntity>().HasQueryFilter(u => u.CreatedBy == _authenticatedUser.UserId);
            builder.Entity<CaseEntity>().HasQueryFilter(u => u.CreatedBy == _authenticatedUser.UserId);
            builder.Entity<LawyerMasterEntity>().HasQueryFilter(u => u.CreatedBy == _authenticatedUser.UserId);
            #endregion
        }
    }
}