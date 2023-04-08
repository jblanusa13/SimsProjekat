using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectSims.Domain.Model;
using ProjectSims.Repository;

namespace ProjectSims.Service
{
    public class LocationService
    {
        private LocationRepository repository;
        public LocationService()
        {
            repository = new LocationRepository();
        }

        public Location GetLocation(int id)
        {
            return repository.Get(id);
        }
    }
}
