using DataAccessLayer.DbContexts.Configurations;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.DbContexts
{
    public class WeatherForecastDbContext : DbContext
    {

        public WeatherForecastDbContext()
        {

        }

        public WeatherForecastDbContext(DbContextOptions<WeatherForecastDbContext> options)
            : base(options)
        {
            Database.Migrate();
        }

        //  Use it when adding the first or new migration

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=Your_(localdb);Database=WeatherForecastDb;Trusted_Connection=True;");
        //}


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new WeatherForecastConfiguration());
        }

        public DbSet<WeatherForecast> WeatherForecasts { get; set; }
    }
}
