using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectSims.Model;
using ProjectSims.ModelDAO;
using ProjectSims.Observer;

namespace ProjectSims.Controller
{
    class AccomodationController
    {
        private readonly AccomodationDAO _accommodations;

        public AccomodationController()
        {
            _accommodations = new AccomodationDAO();
        }
        public List<Accomodation> GetAllAccommodations()
        {
            return _accommodations.GetAll();
        }

        public void Subscribe(IObserver observer)
        {
            _accommodations.Subscribe(observer);
        }
    }
}
