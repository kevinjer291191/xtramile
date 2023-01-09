using API.Model.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlServerCe;

namespace API.Model.Implementation.LocationServiceImplementation
{
    public class SQLServerCompact : Interface.ILocationService
    {
        private readonly SqlCeConnection _connection;
        public SQLServerCompact(SqlCeConnection _connection)
        {
            this._connection = _connection;
        }

        public async Task<List<Location.City>> GetCityByCountryCode(string countryCode)
        {

            List<Contract.Location.City> result = new List<Contract.Location.City>();
            string sql = "select city_name from cities where country_code = @code order by country_code asc";


            _connection.Open();
            using (SqlCeCommand command = new SqlCeCommand(sql, _connection))
            {
                command.Parameters.AddWithValue("@countryCode", countryCode);
                SqlCeDataReader reader = command.ExecuteReader();
                while (await reader.ReadAsync())
                {
                    var c = new Contract.Location.City(reader.GetString(0));
                    result.Add(c);
                }
            }

            return result;
        }

        public async Task<List<Location.Country>> GetCountries()
        {

            List<Contract.Location.Country> result = new List<Contract.Location.Country>();
            string sql = "select distinct country_code, country_name from cities order by country_code asc";
            _connection.Open();
            using (SqlCeCommand command = new SqlCeCommand(sql, _connection))
            {
                SqlCeDataReader reader = command.ExecuteReader();
                while (await reader.ReadAsync())
                {
                    var c = new Contract.Location.Country(reader.GetString(0), reader.GetString(1));
                    result.Add(c);
                }
            }

            return result;
        }
    }
}
