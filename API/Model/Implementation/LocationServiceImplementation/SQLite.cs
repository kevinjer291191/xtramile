using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Model.Implementation.LocationServiceImplementation
{
    public class SQLite : Interface.ILocationService
    {
        private readonly SqliteConnection _connection;
        public SQLite(SqliteConnection _connection)
        {
            this._connection = _connection;
        }

        public async Task<List<Contract.Location.City>> GetCityByCountryCode(string countryCode)
        {
            string sql = "select city_name from cities where country_code = @code order by country_code asc";
            _connection.Open();
            List<Contract.Location.City> result = new List<Contract.Location.City>();
            using (SqliteCommand command = new SqliteCommand(sql, _connection))
            {
                command.Parameters.AddWithValue("@code", countryCode);
                SqliteDataReader reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    var c = new Contract.Location.City(reader.GetString(0));
                    result.Add(c);

                }
            }
            _connection.Close();
            return result;
        }

        public async Task<List<Contract.Location.Country>> GetCountries()
        {
            string sql = "select distinct country_code, country_name from cities order by country_code asc";
            _connection.Open();
            List<Contract.Location.Country> result = new List<Contract.Location.Country>();
            using (SqliteCommand command = new SqliteCommand(sql, _connection))
            {
                SqliteDataReader reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    var c = new Contract.Location.Country(reader.GetString(0), reader.GetString(1));
                    result.Add(c);
                }
            }
            _connection.Close();
            return result;
        }
    }
}
