using Microsoft.EntityFrameworkCore;
using TurboAz.Models.Entities;

namespace TurboAz.Models.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
            : base()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer("Data Source=.;Database=TurboAz;User Id=sa;Password=query;Encrypt=false;App=Orm",
                opt =>
                {
                    opt.MigrationsHistoryTable("MigrationHistory");
                });
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
           
        }

        public DbSet<Announcement> Announcements { get; set; }
        public DbSet<Marka> Marks { get; set; }
        public DbSet<Model> Models { get; set; }


    }
}


