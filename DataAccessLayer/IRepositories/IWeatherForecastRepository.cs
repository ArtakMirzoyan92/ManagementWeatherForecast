using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.IRepositories
{
    public interface IWeatherForecastRepository
    {
        Task<List<WeatherForecast>> GetAllForecastsByDayAsync(DateTime day);
        Task<List<WeatherForecast>> GetWarmDaysAsync(DateTime toDay);
        Task<WeatherForecast> WeatherExistByDateAsync(DateTime date);
        Task<bool> AddAsync(List<WeatherForecast> weatherForecasts);
        Task<bool> UpdateAsync(WeatherForecast weatherForecast);
        Task<bool> DeleteAsync(DateTime date);
    }
}
