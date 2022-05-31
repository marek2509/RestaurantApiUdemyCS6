namespace RestaurantApiUdemyCS6
{
    public interface IWeatherForecastService
    {
        IEnumerable<WeatherForecast> Get(int results, int min, int max);
    }
}
