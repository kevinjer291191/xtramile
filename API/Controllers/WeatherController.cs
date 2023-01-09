using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    [Route("api/weather")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
		private readonly IMemoryCache _cache;
		private readonly ILogger<WeatherController> _logger;

		public WeatherController(ILogger<WeatherController> _logger, IMemoryCache _cache)
		{
			this._cache = _cache;
			this._logger = _logger;

		}
		[HttpGet("city/{cityName}")]
		public async Task<Model.Contract.Weather> GetWeatherByCityName(string cityName)
		{
			string cache_key = $"api:weather:city:{cityName}";
			if (!_cache.TryGetValue(cache_key, out Model.Contract.Weather value))
			{
				//select weather implementation option, For this case. we're using OpenWeather 
				Model.Interface.IWeatherService weatherService = new Model.Implementation.WeatherServiceImplementation.OpenWeather(
				new RestSharp.RestClient(
						AppConfig.OPENWEATHER_API_BASE_URL == null ? "" : AppConfig.OPENWEATHER_API_BASE_URL
					));

				value = await weatherService.GetWeatherByCityName(cityName);
				//cache ttl not long to make sure repeated same city location doesn't call API again if it's on short timeframe 
				_cache.Set(cache_key, value, TimeSpan.FromSeconds(30));
			}
			return value;
		}
	}
}
