using ProjectSims.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSims.Domain.RepositoryInterface
{
    public interface ITourRatingRepository
    {
        public void Create(TourAndGuideRating tourRating);
        public void Update(TourAndGuideRating tourRating);
        public void Remove(TourAndGuideRating tourRating);
        public List<TourAndGuideRating> GetAll();
        public int GetNextId();
    }
}
