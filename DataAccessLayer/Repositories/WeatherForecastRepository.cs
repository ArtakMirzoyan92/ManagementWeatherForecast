using DataAccessLayer.DbContexts;
using DataAccessLayer.Entities;
using DataAccessLayer.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace DataAccessLayer.Repositories
{
    public class WeatherForecastRepository : IWeatherForecastRepository
    {

        public readonly WeatherForecastDbContext _dbContext;

        public WeatherForecastRepository(WeatherForecastDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<WeatherForecast>> GetAllForecastsByDayAsync(DateTime day)
        {
            return await _dbContext.WeatherForecasts.Where(x => x.Date.Date == day.Date)
                                                    .ToListAsync();
        }

        public async Task<WeatherForecast> WeatherExistByDateAsync(DateTime date)
        {
            return await _dbContext.WeatherForecasts.FirstOrDefaultAsync(x => x.Date == date);
        }

        public async Task<List<WeatherForecast>> GetWarmDaysAsync(DateTime fromDay)
        {
            var warmDays = new List<WeatherForecast>();
            if (_dbContext.WeatherForecasts.Any())
            {
                var avgDays = await _dbContext.WeatherForecasts.Where(x => x.Date >= fromDay)
                                                               .AverageAsync(x => x.TemperatureC);

                warmDays = await _dbContext.WeatherForecasts.Where(x => x.Date >= fromDay && x.TemperatureC >= (int)avgDays)
                                                                .OrderByDescending(x => x.TemperatureC)
                                                                .ToListAsync();
            }
            return warmDays;
        }

        public async Task<bool> AddAsync(List<WeatherForecast> weatherForecasts)
        {
            await _dbContext.WeatherForecasts.AddRangeAsync(weatherForecasts);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(WeatherForecast weatherForecast)
        {
            _dbContext.WeatherForecasts.Update(weatherForecast);

            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(DateTime date)
        {
            var oldData = await _dbContext.WeatherForecasts.Where(x => x.Date.Date < date.Date).ToListAsync();
            if (oldData.Count > 0)
            {
                _dbContext.WeatherForecasts.RemoveRange(oldData);

                await _dbContext.SaveChangesAsync();
            }
            return true;
        }
    }
}
