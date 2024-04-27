namespace BusinessLayer.Models
{
    public class WeatherForecastDtoResponse
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string HourlyForecast { get; set; }
        public int TemperatureC { get; set; }
        public int TemperatureF { get; set; }
        public string Summary { get; set; }
    }
}
