using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessLayer.DbContexts.Configurations
{
    public class WeatherForecastConfiguration : IEntityTypeConfiguration<WeatherForecast>
    {
        public void Configure(EntityTypeBuilder<WeatherForecast> builder)
        {

            {
                builder.HasKey(x => x.Id)
                       .HasName("PK_WeatherForecast_Id");

                builder.Property(x => x.City)
                       .IsRequired()
                       .HasMaxLength(200);

                builder.Property(x => x.Date)
                       .HasColumnType("datetime");

                builder.HasIndex(x => x.Date)
                       .IsUnique()
                       .HasDatabaseName("Index_WeatherForecast_Date");

                builder.Property(x => x.TemperatureC)
                       .IsRequired();

                builder.Property(x => x.TemperatureF)
                       .IsRequired();

                builder.Property(x => x.Summary)
                       .IsRequired()
                       .HasMaxLength(50);

            }
        }
    }
}