using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Model.Interface
{
	public interface IWeatherService
	{
		Task<Contract.Weather> GetWeatherByCityName(string cityName);
	}
}




