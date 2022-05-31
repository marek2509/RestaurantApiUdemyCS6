namespace RestaurantApiUdemyCS6
{
    public class WeatherForecastService : IWeatherForecastService
    {
        private static readonly string[] Summaries = new[]
{
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };
        public IEnumerable<WeatherForecast> Get(int results, int min, int max)
        {
            return Enumerable.Range(1, results).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(min, max),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
    .ToArray();
        }

    }
}
