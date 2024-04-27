using BusinessLayer.IServices;
using BusinessLayer.Models;
using Microsoft.AspNetCore.Mvc;

namespace ManagementWeatherForecast.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherForecastController : ControllerBase
    {

        private readonly IWeatherForecastService _weatherForecastService;

        public WeatherForecastController(IWeatherForecastService weatherForecastService)
        {
            _weatherForecastService = weatherForecastService;
        }

        [HttpGet("{day}")]
        public async Task<IActionResult> GetByDay(DateTime day)
        {
            var result = await _weatherForecastService.GetAllForecastsByDayAsync(day.Date);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest();
        }

        [HttpGet("GetWarmDays")]
        public async Task<IActionResult> GetWarmDays()
        {
            var warmDays = await _weatherForecastService.GetUpcomingWarmDaysAsync();
            if (warmDays != null)
            {
                return Ok(warmDays);
            }
            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> Post(List<WeatherForecastDtoForCreate> weatherForecastDto)
        {
            var isCreated = await _weatherForecastService.AddWeatherForecastAsync(weatherForecastDto);

            if (isCreated)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> Put(WeatherForecastDtoForUpdate weatherForecastDto)
        {
            var isUpdated = await _weatherForecastService.UpdateByDateAsync(weatherForecastDto);
            if (isUpdated)
            {
                return NoContent();
            }
            return BadRequest();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete()
        {
            var isDeleted = await _weatherForecastService.DeleteOldWeatherDataAsync();
            if (isDeleted)
            {
                return NoContent();
            }
            return BadRequest();
        }

    }
}
