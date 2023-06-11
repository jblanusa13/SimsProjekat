using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectSims.Domain.Model;
using ProjectSims.Domain.RepositoryInterface;
using ProjectSims.FileHandler;
using ProjectSims.Observer;

namespace ProjectSims.Repository
{
    public class LocationRepository : ILocationRepository
    {
        private LocationFileHandler fileHandler;
        private List<Location> locations;
        public LocationRepository()
        {
            fileHandler = new LocationFileHandler();
            locations = fileHandler.Load();
        }
        public Location GetById(int id)
        {
            return locations.Find(l => l.Id == id);
        }

        public int NextId()
        {
            return locations.Max(a => a.Id) + 1;
        }

        public void Create(Location location)
        {
            location.Id = NextId();
            locations.Add(location);
            fileHandler.Save(locations);
        }

        public void Remove(Location location)
        {
            locations.Remove(location);
            fileHandler.Save(locations);
        }

        public void Update(Location location)
        {
            int index = locations.FindIndex(a => location.Id == a.Id);
            if (index != -1)
            {
                locations[index] = location;
            }
            fileHandler.Save(locations);
        }

        public List<Location> GetAll()
        {
            return locations;
        }

        public List<string> GetAllCities()
        {
            List<string> cities = new List<string>();
            foreach(Location location in GetAll())
            {
                if (!cities.Contains(location.City))
                {
                    cities.Add(location.City);
                }
            }

            return cities;
        }
        public List<string> GetAllCountries()
        {
            List<string> countries = new List<string>();
            foreach (Location location in GetAll())
            {
                if (!countries.Contains(location.Country))
                {
                    countries.Add(location.Country);
                }
            }

            return countries;
        }

        public List<string> GetAllCitiesByCountry(string country)
        {
            List<string> cities = new List<string>();
            foreach (Location location in GetAll())
            {
                if (location.Country.Equals(country))
                {
                    cities.Add(location.City);
                }
            }

            return cities;
        }

        public List<string> GetAllCountriesByCity(string city)
        {
            List<string> countries = new List<string>();
            foreach (Location location in GetAll())
            {
                if (location.City.Equals(city))
                {
                    countries.Add(location.Country);
                }
            }

            return countries;
        }

        public int GetIdByLocation(string location)
        {
            string city = location.Split(",")[0];
            string country = location.Split(",")[1];

            foreach (Location l in locations)
            {
                if (city == l.City && country == l.Country)
                {
                    return l.Id;
                }
            }
            return -1;
        }

        public void Add(string location)
        {
            if (!Exist(location))
            {
                Location loc = new Location(NextId(), location.Split(",")[0], location.Split(",")[1]);
                Create(loc);
            }
        }

        public List<string> GetAllLocationsString()
        {
            List<string> locations = new List<string>();
            foreach(Location location in GetAll())
            {
                locations.Add(location.City + ", " + location.Country);
            }
            return locations;
        }

        public Location GetLocationByCityAndCountry(string city, string country)
        {
            return locations.Find(l => l.City == city && l.Country == country);
        }

        public bool Exist(string location)
        {
            string city = location.Split(",")[0];
            string country = location.Split(",")[1];

            foreach (Location l in locations)
            {
                if (city == l.City && country == l.Country)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
