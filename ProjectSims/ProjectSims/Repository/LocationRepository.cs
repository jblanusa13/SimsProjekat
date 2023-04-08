using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectSims.Domain.Model;
using ProjectSims.FileHandler;

namespace ProjectSims.Repository
{
    public class LocationRepository
    {
        private LocationFileHandler fileHandler;
        private List<Location> locations;
        public LocationRepository()
        {
            fileHandler = new LocationFileHandler();
            locations = fileHandler.Load();
        }

        public Location Get(int id)
        {
            return locations.Find(l => l.Id == id);
        }
    }
}
