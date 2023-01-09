using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Model.Interface
{
    public interface ILocationService
    {
        Task<List<Contract.Location.Country>> GetCountries();
        Task<List<Contract.Location.City>> GetCityByCountryCode(string countryCode);
    }
}
