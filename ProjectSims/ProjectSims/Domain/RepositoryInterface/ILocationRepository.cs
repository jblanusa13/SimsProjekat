using ProjectSims.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSims.Domain.RepositoryInterface
{
    public interface ILocationRepository : IGenericRepository<Location, int>
    {
        public int GetIdByLocation(string location);
        public void Add(string location);
        public bool Exist(string location);

        public List<string> GetAllCities();
        public List<string> GetAllCountries();
        public List<string> GetAllCitiesByCountry(string country);
        public List<string> GetAllCountriesByCity(string city);

        public List<string> GetAllLocationsString();
        public Location GetLocationByCityAndCountry(string city, string country);
    }
}
