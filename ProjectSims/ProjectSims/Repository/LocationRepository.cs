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

        public int GetLocationId(string location)
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

        public int Add(string location)
        {
            int id = -1;
            if (!Exist(location))
            {
                id = NextId();
                Location loc = new Location(id, location.Split(",")[0], location.Split(",")[1]);
                locations.Add(loc);
                fileHandler.Save(locations);
            }
            return id;
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
