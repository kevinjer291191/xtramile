using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Text.Json.Serialization;
namespace API.Model.Contract
{ 
    public class Weather
    {
        public Weather()
        {
        }

        [JsonPropertyName("Location")]
        public string? Location { get; set; }

        [JsonPropertyName("Time")]
        public string? Time { get; set; }

        [JsonPropertyName("Description")]
        public string? Description { get; set; }

        [JsonPropertyName("WindDetail")]
        public Wind? WindDetail { get; set; }

        [JsonPropertyName("Visibility")]
        public double? Visibility { get; set; }

        [JsonPropertyName("SkyCondition")]
        public string? SkyCondition { get; set; }

        [JsonPropertyName("TemperatureF")]
        public double? TemperatureF { get; set; }

        [JsonPropertyName("TemperatureC")]
        public double? TemperatureC
        {
            get
            {
                return (this.TemperatureF - 32) * 0.5556;
            }
        }

        [JsonPropertyName("TemperatureFMin")]
        public double? TemperatureFMin { get; set; }

        [JsonPropertyName("TemperatureCMin")]
        public double? TemperatureCMin
        {
            get
            {
                return (this.TemperatureFMin - 32) * 0.5556;
            }
        }

        [JsonPropertyName("TemperatureFMax")]
        public double? TemperatureFMax { get; set; }

        [JsonPropertyName("TemperatureCMax")]
        public double? TemperatureCMax
        {
            get
            {
                return (this.TemperatureFMax - 32) * 0.5556;
            }
        }

        [JsonPropertyName("Humidity")]
        public double? Humidity { get; set; }

        [JsonPropertyName("Pressure")]
        public double? Pressure { get; set; }
        [JsonPropertyName("Icon")]

        public string? Icon { get; set; }


        public class Wind
        {
            [JsonPropertyName("Speed")]
            public double? Speed { get; set; }
            [JsonPropertyName("Degree")]
            public double? Degree { get; set; }
        }

    }
}

