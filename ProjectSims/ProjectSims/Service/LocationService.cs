using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectSims.Domain.Model;
using ProjectSims.Domain.RepositoryInterface;
using ProjectSims.Repository;
using ProjectSims.WPF.View.OwnerView;

namespace ProjectSims.Service
{
    public class LocationService
    {
        private ILocationRepository repository;
        public LocationService()
        {
            repository = Injector.CreateInstance<ILocationRepository>();
        }

        public Location GetLocation(int id)
        {
            return repository.GetById(id);
        }
        public int GetIdByLocation(string location)
        {
            return repository.GetIdByLocation(location);
        }

        public void Add(string location)
        {
            repository.Add(location);
        }
        public List<string> GetAllLocationsString()
        {
            return repository.GetAllLocationsString();
        }

        public Location GetLocationByCityAndCountry(string city, string country)
        {
            return repository.GetLocationByCityAndCountry(city, country);
        }
        public List<string> GetAllCities()
        {
            return repository.GetAllCities();
        }

        public List<string> GetAllCountries()
        {
            return repository.GetAllCountries();
        }
        public List<string> GetAllCitiesByCountry(string country)
        {
            return repository.GetAllCitiesByCountry(country);
        }
        public List<string> GetAllCountriesByCity(string city)
        {
            return repository.GetAllCountriesByCity(city);
        }
    }
}
