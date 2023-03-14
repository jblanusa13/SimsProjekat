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
        public void Save(string name, string location, string description,string language, string maxNumberGuests, string startKeyPoint, string finishKeyPoint, List<string> otherKeyPoints, string tourStarts, string duration,string images)
        {             
            List<string> keyPoints = new List<string>();
            keyPoints.Add(startKeyPoint);
            keyPoints.AddRange(otherKeyPoints);
            keyPoints.Add(finishKeyPoint);

            List<string> imageList = new List<string>();
            foreach (string image in images.Split(','))
            {
                imageList.Add(image);
            }


            foreach (string tourStart in tourStarts.Split(','))
            {
                _tourDAO.Save(name, location, description, language, Convert.ToInt32(maxNumberGuests), keyPoints, DateTime.ParseExact(tourStart, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture), Convert.ToDouble(duration), imageList);
            }         
        }

        public void Subscribe(IObserver observer)
        {
            _tourDAO.Subscribe(observer);
        }

    }
}
