using NUnit.Framework;
using System.Threading.Tasks;
using API.Model;
using System;
using Moq;
using API.Model.Interface;

namespace UnitTest
{
    [TestFixture]
    public class LocationTests
    {
        [Test]
        public async Task Positive_GetCountries()
        {

            var mock = new Mock<ILocationService>();
            mock.Setup(x => x.GetCountries())
                .ReturnsAsync(
                    new System.Collections.Generic.List<API.Model.Contract.Location.Country>()
                    {
                        {
                            new API.Model.Contract.Location.Country("ID", "Indonesia")
                        },
                        {
                            new API.Model.Contract.Location.Country("JP", "Japan")
                        },
                        {
                            new API.Model.Contract.Location.Country("GB", "Great Britain")
                        }
                    }
                ); ;

            var response = await mock.Object.GetCountries();

            Assert.That(response.Count == 3);
        }

        [Test]
        public async Task Positive_GetCityByCountryCode()
        {
            var mock = new Mock<ILocationService>();
            mock.Setup(x => x.GetCityByCountryCode("ID"))
                .ReturnsAsync(
                    new System.Collections.Generic.List<API.Model.Contract.Location.City>()
                    {
                        {
                            new API.Model.Contract.Location.City("Jakarta")
                        },
                        {
                            new API.Model.Contract.Location.City("Bandung")
                        },
                        {
                            new API.Model.Contract.Location.City("Surabaya")
                        }
                    }
                ); ;

            var response = await mock.Object.GetCityByCountryCode("ID");

            Assert.That(response.Count == 3);
        }

    }
}