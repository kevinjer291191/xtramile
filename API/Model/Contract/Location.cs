using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace API.Model.Contract
{
    public class Location
    {
        public class Country
        {
            public Country(string code, string name)
            {
                this.CountryCode = code;
                this.CountryName = name;
            }

            [JsonPropertyName("CountryCode")]
            public string? CountryCode { get; set; }
            [JsonPropertyName("CountryName")]
            public string? CountryName { get; set; }
        }
        public class City
        {
            public City( string name)
            {
                this.CityName = name;
            }
            [JsonPropertyName("CityName")]
            public string? CityName { get; set; }
        }
    }
}
