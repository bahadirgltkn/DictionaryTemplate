using DictionaryTemplate.Api.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace DictionaryTemplate.Infrastructure.Persistance.Contexts
{
    public class DictionaryTemplateContext : DbContext
    {
        public const string DEFAULT_SCHEMA = "dbo";

        public DictionaryTemplateContext()
        {
            
        }
        public DictionaryTemplateContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> User { get; set; }
        public DbSet<Title> Titles { get; set; }
        public DbSet<TitleVote> TitleVotes { get; set; }
        public DbSet<TitleFavorite> TitleFavorites { get; set; }
        public DbSet<Entry> Entries { get; set; }
        public DbSet<EntryVote> EntryVotes { get; set; }
        public DbSet<EntryFavorite> EntryFavorites { get; set; }
        public DbSet<EmailConfirmation> EmailConfirmations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var conn = "Server=DESKTOP-BDT2C5E\\BHDRGLTKN;Database=dictionarytemplate;Trusted_Connection=SSPI;Encrypt=false;TrustServerCertificate=true";

                optionsBuilder.UseSqlServer(conn, opt =>
                {
                    opt.EnableRetryOnFailure();
                });
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // IEntityTypeConfiguration<T>
            // to automatically add entity configurations
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public override int SaveChanges()
        {
            OnBeforeSave();
            return base.SaveChanges();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            OnBeforeSave();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            OnBeforeSave();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            OnBeforeSave();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void OnBeforeSave()
        {
            var addedEntities = ChangeTracker.Entries()
                                    .Where(i => i.State == EntityState.Added)
                                    .Select(i => (BaseEntity)i.Entity);

            PreparedAddedEntities(addedEntities);
        }

        private void PreparedAddedEntities(IEnumerable<BaseEntity> entities)
        {
            foreach (var entity in entities)
            {
                if (entity.CreatedAt == DateTime.MinValue)
                    entity.CreatedAt = DateTime.Now;
            }
        }

    }
}
