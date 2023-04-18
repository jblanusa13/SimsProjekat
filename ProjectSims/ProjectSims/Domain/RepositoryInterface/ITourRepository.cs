using ProjectSims.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSims.Domain.RepositoryInterface
{
    public interface ITourRepository
    {
        public void Create(Tour tour);
        public void Update(Tour tour);
        public void Remove(Tour tour);
        public List<Tour> GetAll();
        public int NextId();

    }
}
