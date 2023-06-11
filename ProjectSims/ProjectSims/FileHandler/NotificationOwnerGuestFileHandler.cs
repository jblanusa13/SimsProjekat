using ProjectSims.Domain.Model;
using ProjectSims.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSims.FileHandler
{
    class NotificationOwnerGuestFileHandler
    {
        private const string FilePath = "../../../Resources/Data/notificationOwnerGuest.csv";

        private Serializer<NotificationOwnerGuest> _serializer;
        public NotificationOwnerGuestFileHandler()
        {
            _serializer = new Serializer<NotificationOwnerGuest>();
        }
        public List<NotificationOwnerGuest> Load()
        {
            return _serializer.FromCSV(FilePath);
        }
        public void Save(List<NotificationOwnerGuest> notifications)
        {
            _serializer.ToCSV(FilePath, notifications);
        }
    }
}
