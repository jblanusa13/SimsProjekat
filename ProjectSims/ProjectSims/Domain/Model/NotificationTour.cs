using ProjectSims.Serializer;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSims.Domain.Model
{
    public class NotificationTour : ISerializable
    {
        public int Id { get; set; }
        public int Guest2Id { get; set; }
        public int GuideId { get; set; }
        public int TourId { get; set; }
        public string ContentNotification { get; set; }
        public DateTime DateSentNotification { get; set; }
        public bool Seen { get; set; }
        public NotificationTour()
        {

        }
        public NotificationTour(int id, int guest2Id, int guideId, int tourId, string contentNotification, DateTime dateSentNotification, bool seen)
        {
            Id = id;
            Guest2Id = guest2Id;
            GuideId = guideId;
            TourId = tourId;
            ContentNotification = contentNotification;
            DateSentNotification = dateSentNotification;
            Seen = seen;
        }

        public string[] ToCSV()
        {
            string[] csvvalues = { Id.ToString(), Guest2Id.ToString(), GuideId.ToString(), TourId.ToString(), ContentNotification, DateSentNotification.ToString(), Seen.ToString() };
            return csvvalues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Guest2Id = Convert.ToInt32(values[1]);
            GuideId = Convert.ToInt32(values[2]);
            TourId = Convert.ToInt32(values[3]);
            ContentNotification = values[4];
            DateSentNotification = DateTime.ParseExact(values[5], "dd/MM/yyyy", CultureInfo.InvariantCulture);
            Seen = Convert.ToBoolean(values[6]);
        }
    }
}
