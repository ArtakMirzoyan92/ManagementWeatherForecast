namespace BusinessLayer.Models
{
    public class ResponseWarmDays
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string Date { get; set; }
        public int TemperatureC { get; set; }
        public int TemperatureF { get; set; }
        public string Summary { get; set; }
    }
}
