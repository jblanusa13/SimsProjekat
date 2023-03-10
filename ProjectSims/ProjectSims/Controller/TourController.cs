using ProjectSims.FileHandler;
using ProjectSims.Model;
using ProjectSims.ModelDAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;
using ProjectSims.Observer;
using System.Globalization;

namespace ProjectSims.Controller
{
    public class TourController
    {
        private readonly TourDAO _tourDAO;
        public TourController()
        {
            _tourDAO = new TourDAO();
        }
        public void Save(string name, string location, string language, string maxNumberGuests, string duration, string tourStart, string description)
        {
             
             _tourDAO.Save(name,location,language,Convert.ToInt32(maxNumberGuests),Convert.ToInt32(duration), DateTime.ParseExact(tourStart,"MM/dd/yyyy HH:mm", CultureInfo.InvariantCulture), description);
            
        }

        public void Subscribe(IObserver observer)
        {
            _tourDAO.Subscribe(observer);
        }

    }
}
