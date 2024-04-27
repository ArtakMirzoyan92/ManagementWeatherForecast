using BusinessLayer.Models;

namespace BusinessLayer.IServices
{
    public interface IWeatherForecastService
    {
        Task<List<WeatherForecastDtoResponse>> GetAllForecastsByDayAsync(DateTime day);
        Task<List<ResponseWarmDays>> GetUpcomingWarmDaysAsync();
        Task<bool> AddWeatherForecastAsync(List<WeatherForecastDtoForCreate> weatherForecasts);
        Task<bool> UpdateByDateAsync(WeatherForecastDtoForUpdate weatherForecast);
        Task<bool> DeleteOldWeatherDataAsync();
    }
}
