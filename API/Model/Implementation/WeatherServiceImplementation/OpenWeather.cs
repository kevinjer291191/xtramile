using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace API.Model.Implementation.WeatherServiceImplementation
{
    public class OpenWeather : Interface.IWeatherService
    {

        private RestSharp.RestClient _restClient;
        public OpenWeather(RestSharp.RestClient _restClient)
        {
            this._restClient = _restClient;
        }

        public async Task<Contract.Weather> GetWeatherByCityName(string cityName)
        {
            Contract.Weather weather;

            //add units imperial to set to fahrenheit for Task's purpose
            string endpoint = $"?q={cityName},uk&APPID={AppConfig.OPENWEATHER_API_KEY}&units=imperial";

            var request = new RestSharp.RestRequest(endpoint, RestSharp.Method.Get);

            var response = await _restClient.ExecuteAsync(request);

            if (!response.IsSuccessful)
            {
                throw new Exception("There's error in handling request");
            }

            dynamic obj = JObject.Parse(response.Content);
            weather = new Contract.Weather()
            {
                Description = (string?)obj.weather[0].main,
                Humidity = (double?)obj.main.humidity,
                Location = cityName,
                Pressure = (double?)obj.main.pressure,
                SkyCondition = (string?)obj.weather[0].description,
                TemperatureF = (double?)obj.main.temp,
                TemperatureFMin = (double?)obj.main.temp_min,
                TemperatureFMax = (double?)obj.main.temp_max,
                Time = DateTime.Now.ToShortTimeString(),
                Visibility = (double?)obj.visibility,
                WindDetail = new Contract.Weather.Wind()
                {
                    Degree = (double?)obj.wind.deg,
                    Speed = (double?)obj.wind.speed,
                },
                Icon = $"http://openweathermap.org/img/wn/{(string?)obj.weather[0].icon}@2x.png"
            };

            return weather;
        }

    }
}

