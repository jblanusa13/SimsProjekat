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
        public void Save(string name, string location, string description,string language, string maxNumberGuests, string startKeyPoint, string finishKeyPoint, List<string> otherKeyPoints, string tourStart, string duration,string images)
        {             
            List<string> keyPoints = new List<string>();
            keyPoints.Add(startKeyPoint);
            keyPoints.AddRange(otherKeyPoints);
            keyPoints.Add(finishKeyPoint);

             _tourDAO.Save(name, location, description, language, Convert.ToInt32(maxNumberGuests), keyPoints, DateTime.ParseExact(tourStart,"MM/dd/yyyy HH:mm", CultureInfo.InvariantCulture), Convert.ToDouble(duration), images);          
        }

        public void Subscribe(IObserver observer)
        {
            _tourDAO.Subscribe(observer);
        }

    }
}
