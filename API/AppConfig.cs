using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API
{
    public static class AppConfig
    {
        public static string OPENWEATHER_API_KEY { get; set; } = "";
        public static string OPENWEATHER_API_BASE_URL { get; set; } = "";
        public static string LOCATION_DB_SQLITE { get; set; } = "";
        public static string LOCATION_DB_SQLCOMPACT { get; set; } = "";
    }
}

