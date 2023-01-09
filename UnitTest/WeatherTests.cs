using NUnit.Framework;
using System.Threading.Tasks;
using API.Model;
using System;
using Moq;
using API.Model.Interface;

namespace UnitTest
{
    [TestFixture]
    public class WeatherTests
    {

        readonly string valid_location = "Jakarta, ID";
        readonly string invalid_location = "Jakawtaaaaa, DI";


        [Test]
        public async Task Positive_GetCurrentWeather_ValidCity_Static()
        {

            var mock = new Mock<IWeatherService>();
            mock.Setup(x => x.GetWeatherByCityName(valid_location))
                .ReturnsAsync(new API.Model.Contract.Weather
                {
                    TemperatureF = 87.75,
                    Humidity = 100,
                    Location = valid_location,
                    Visibility = 2000,
                    Pressure = 100,
                    SkyCondition = "Raining",
                    WindDetail = new API.Model.Contract.Weather.Wind
                    {
                        Degree = 1,
                        Speed = 1
                    }
                });

            var response = await mock.Object.GetWeatherByCityName(valid_location);


            Assert.That(response.Visibility, Is.EqualTo(2000));
            Assert.That(response.TemperatureF, Is.EqualTo(87.75));

            Assert.That(response.Humidity, Is.EqualTo(100));
            Assert.That(response.Pressure, Is.EqualTo(100));

            Assert.That(response.SkyCondition, Is.EqualTo("Raining"));
            Assert.That(response.WindDetail.Speed, Is.EqualTo(1));
            Assert.That(response.WindDetail.Degree, Is.EqualTo(1));

            Assert.That(response.TemperatureC, Is.EqualTo(30.9747));
            Assert.That(response.Location, Is.EqualTo(valid_location));
        }

        [Test]
        public async Task Positive_GetCurrentWeather_ValidCity_RandomConversion()
        {
            var mock = new Mock<IWeatherService>();
            Random rand = new Random();
            mock.Setup(x => x.GetWeatherByCityName(valid_location))
                .ReturnsAsync(new API.Model.Contract.Weather
                {
                    TemperatureF = rand.Next(1, 300),
                    TemperatureFMin = rand.Next(1, 300),
                    TemperatureFMax = rand.Next(1, 300)
                });

            var response = await mock.Object.GetWeatherByCityName(valid_location);

            Assert.That(response.TemperatureC, Is.EqualTo((response.TemperatureF - 32) * 0.5556));
            Assert.That(response.TemperatureCMin, Is.EqualTo((response.TemperatureFMin - 32) * 0.5556));
            Assert.That(response.TemperatureCMax, Is.EqualTo((response.TemperatureFMax - 32) * 0.5556));
        }

        [Test]
        public async Task Negative_GetCurrentWeather_InvalidCity()
        {
            var mock = new Mock<IWeatherService>();
            Random rand = new Random();
            mock.Setup(x => x.GetWeatherByCityName(invalid_location)).Throws(new Exception("There's error in handling request"));

            try
            {
                var response = await mock.Object.GetWeatherByCityName(invalid_location);
                Assert.Fail("API Should be throw error. Fail");

            }
            catch (Exception e)
            {
                Assert.That(e.Message, Is.EqualTo("There's error in handling request"));
            }
        }


        [Test]
        public async Task Negative_GetCurrentWeather_API_IsDown()
        {
            var mock = new Mock<IWeatherService>();
            Random rand = new Random();
            mock.Setup(x => x.GetWeatherByCityName(invalid_location)).Throws(new Exception("API IS Down!"));

            try
            {
                var response = await mock.Object.GetWeatherByCityName(invalid_location);
                Assert.Fail("API Should be down. Fail");

            }
            catch (Exception e)
            {
                Assert.That(e.Message, Is.EqualTo("API IS Down!"));
            }

        }
    }
}