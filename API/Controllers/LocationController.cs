using System;
using System.Collections.Generic;
using System.Data.SqlServerCe;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    [Route("api/location")]
    [ApiController]
    public class LocationController : ControllerBase
    {
		private readonly IMemoryCache _cache;
		private readonly ILogger<LocationController> _logger;
		private readonly Model.Interface.ILocationService _locationService;
		private enum DataSource
        {
			SQLite,
			SQLCompact
        }
		public LocationController(ILogger<LocationController> _logger, IMemoryCache _cache)
		{
			this._cache = _cache;
			this._logger = _logger;
			//set the implementation here, what data source you're going to use. for now default using litedb. 
			//change this enum to change the implementation. SQLite should be more stable because there's not much dependencies for using this dll
			DataSource implement_using = DataSource.SQLite;

            switch (implement_using)
            {
                case DataSource.SQLite:
					SqliteConnection sqliteConnection = new SqliteConnection($"Data Source={AppConfig.LOCATION_DB_SQLITE}");
					_locationService = new Model.Implementation.LocationServiceImplementation.SQLite(sqliteConnection);
					break;
                case DataSource.SQLCompact:
					SqlCeConnection sqlCeConnection = new SqlCeConnection($"Data Source={AppConfig.LOCATION_DB_SQLCOMPACT};Password=xtramilepassword");
					_locationService = new Model.Implementation.LocationServiceImplementation.SQLServerCompact(sqlCeConnection);
					break;
                default:
                    break;
            }

            



		}
		[HttpGet("countries")]
		public async Task<List<Model.Contract.Location.Country>> GetCountries()
		{
			string cache_key = $"api:location:countries";
			if (!_cache.TryGetValue(cache_key, out List<Model.Contract.Location.Country> value))
			{
				value = await _locationService.GetCountries();
				_cache.Set(cache_key, value, TimeSpan.FromHours(1));
			}
			return value;
		}
		[HttpGet("cities/{countryCode}")]
		public async Task<List<Model.Contract.Location.City>> GetCityByCountryCode(string countryCode)
		{
			string cache_key = $"api:location:cities:{countryCode}";
			if (!_cache.TryGetValue(cache_key, out List<Model.Contract.Location.City> value))
			{
				value = await _locationService.GetCityByCountryCode(countryCode);
				_cache.Set(cache_key, value, TimeSpan.FromHours(1));
			}
			return value;
		}

	}
}
