using WashingCar.DAL.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace WashingCar.DAL
{
    public class DataBaseContext : IdentityDbContext<User>
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {
        }

        #region Properties
        public DbSet<Service> Services { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        #endregion



        //Vamos a crear un índice para la tabla Countries
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Service>().HasIndex(c => c.Name).IsUnique();
             modelBuilder.Entity<Vehicle>().HasIndex(v => v.Id).IsUnique();
           /* modelBuilder.Entity<Service>()
            .HasMany(s => s.Vehicles)
            .WithOne(v => v.Service)
            .HasForeignKey(v => v.ServiceId);*/
        }
    }
}
