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

        public DbSet<Service> Services { get; set; }



        //Vamos a crear un índice para la tabla Countries
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Service>().HasIndex(c => c.Name).IsUnique();
            // modelBuilder.Entity<Vehicle>().HasIndex("Id", "ServiceId").IsUnique();
            //modelBuilder.Entity<VehicleDetails>().HasIndex("Id", "VehicleId").IsUnique(); 
        }
    }
}
