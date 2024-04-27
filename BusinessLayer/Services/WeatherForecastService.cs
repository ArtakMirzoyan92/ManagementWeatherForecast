using BusinessLayer.IServices;
using BusinessLayer.Models;
using DataAccessLayer.Entities;
using DataAccessLayer.IRepositories;
using Mapster;

namespace BusinessLayer.Services
{
    public class WeatherForecastService : IWeatherForecastService
    {
        public readonly IWeatherForecastRepository _repository;

        public WeatherForecastService(IWeatherForecastRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<WeatherForecastDtoResponse>> GetAllForecastsByDayAsync(DateTime day)
        {
            var list = await _repository.GetAllForecastsByDayAsync(day);
            return list.Select(x => new WeatherForecastDtoResponse
            {

                Id = x.Id,
                City = x.City,
                HourlyForecast = x.Date.ToString("HH:mm"),
                TemperatureC = x.TemperatureC,
                TemperatureF = x.TemperatureF,
                Summary = x.Summary

            }).ToList();
        }

        public async Task<List<ResponseWarmDays>> GetUpcomingWarmDaysAsync()
        {
            var warmDays = await _repository.GetWarmDaysAsync(DateTime.Now);
            return warmDays.Adapt<List<ResponseWarmDays>>();
        }

        public async Task<bool> AddWeatherForecastAsync(List<WeatherForecastDtoForCreate> weatherForecasts)
        {
            for (int i = 0; i < weatherForecasts.Count; i++)
            {
                if (await _repository.WeatherExistByDateAsync(weatherForecasts[i].Date) != null)
                {
                    return false;
                }
            }

            var curentDate = DateTime.Now.Date;

            if (weatherForecasts.Any(x => x.Date.Date >= curentDate.AddDays(7).Date || x.Date.Date < curentDate))
            {
                return false;
            }

            List<WeatherForecast> createListWeather = weatherForecasts.Adapt<List<WeatherForecast>>();
            
            return await _repository.AddAsync(createListWeather);
        }

        public async Task<bool> UpdateByDateAsync(WeatherForecastDtoForUpdate weatherForecast)
        {

            var weatherByDate = await _repository.WeatherExistByDateAsync(weatherForecast.Date);

            if (weatherByDate == null)
            {
                return false;
            }

            return await _repository.UpdateAsync(weatherForecast.Adapt(weatherByDate));
        }

        public async Task<bool> DeleteOldWeatherDataAsync()
        {
            var oldDate = DateTime.Now.AddDays(-30).Date;

            return await _repository.DeleteAsync(oldDate);
        }
    }
}
