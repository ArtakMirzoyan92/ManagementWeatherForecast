namespace WorkerService
{
    public class OldDataCleanupWorker : BackgroundService
    {
        private readonly ILogger<OldDataCleanupWorker> _logger;

        public OldDataCleanupWorker(ILogger<OldDataCleanupWorker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {

                using var client = new HttpClient();
                var response = await client.DeleteAsync("https://localhost:7261/api/WeatherForecast", stoppingToken);

                if (response.IsSuccessStatusCode)
                {
                    _logger.LogInformation($"Worker deletion successfully at {DateTimeOffset.Now}");
                }
                else
                {
                    _logger.LogError($"Worker deletion failed: {DateTimeOffset.Now}");
                }

                await Task.Delay(TimeSpan.FromDays(1), stoppingToken);
            }
        }
    }
}
