using Microsoft.AspNetCore.Mvc;

namespace LogAnalyticsApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            _logger.LogError("TEST LogError GetWeatherForecast() *********************************");
            _logger.LogCritical("TEST LogCritical GetWeatherForecast() *********************************");
            _logger.LogWarning("TEST LogWarning GetWeatherForecast() *********************************");


            _logger.LogInformation("Start GetWeatherForecast() *********************************");

            try
            {
                var forecasts = Enumerable.Range(1, 5).Select(index =>
                {
                    var forecast = new WeatherForecast
                    {
                        Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                        TemperatureC = Random.Shared.Next(-20, 55),
                        Summary = Summaries[Random.Shared.Next(Summaries.Length)]
                    };

                    // Log ข้อมูลของ forecast แต่ละตัว
                    _logger.LogInformation("Generated forecast: {Date} - Temp: {TemperatureC}°C - {Summary}", forecast.Date, forecast.TemperatureC, forecast.Summary);

                    return forecast;
                }).ToArray();

                _logger.LogInformation("Finished GetWeatherForecast() *********************************");
                return forecasts;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred GetWeatherForecast() *********************************");
                throw; // ส่งต่อ exception เพื่อให้ API ส่งสถานะผิดพลาด
            }

        }
    }
}
