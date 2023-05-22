using ProjectSims.Domain.Model;
using ProjectSims.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSims.FileHandler
{
    class NotificationTourFileHandler
    {
        private const string FilePath = "../../../Resources/Data/notificationtour.csv";

        private Serializer<NotificationTour> _serializer;
        public NotificationTourFileHandler()
        {
            _serializer = new Serializer<NotificationTour>();
        }
        public List<NotificationTour> Load()
        {
            return _serializer.FromCSV(FilePath);
        }
        public void Save(List<NotificationTour> notificationTours)
        {
            _serializer.ToCSV(FilePath, notificationTours);
        }
    }
}
