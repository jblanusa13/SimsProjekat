using ProjectSims.Repository;
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
        public Guest2 Guest2 { get; set; }
        public int GuideId { get; set; }
        public Guide Guide { get; set; }
        public List<int> TourIds { get; set; }
        public string ContentNotification { get; set; }
        public DateTime DateSentNotification { get; set; }
        public bool Seen { get; set; }
        public NotificationTour()
        {
            TourIds = new List<int>();
        }
        public NotificationTour(int id, int guest2Id, int guideId, List<int> tourIds, string contentNotification, DateTime dateSentNotification, bool seen)
        {
            Id = id;
            Guest2Id = guest2Id;
            GuideId = guideId;
            TourIds = tourIds;
            ContentNotification = contentNotification;
            DateSentNotification = dateSentNotification;
            Seen = seen;
        }

        public string[] ToCSV()
        {
            string TourIdArray = "";
            foreach (int tourId in TourIds)
            {
                if (tourId != TourIds.Last())
                {
                    TourIdArray += tourId.ToString() + ",";
                }
            }
            TourIdArray += TourIds.Last().ToString();

            string[] csvvalues = { Id.ToString(), Guest2Id.ToString(), GuideId.ToString(), TourIdArray, ContentNotification, DateSentNotification.ToString("dd/MM/yyyy HH:mm:ss"), Seen.ToString() };
            return csvvalues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Guest2Id = Convert.ToInt32(values[1]);
            Guest2Repository guest2Repository = new();
            Guest2 = guest2Repository.GetById(Convert.ToInt32(values[1]));
            GuideId = Convert.ToInt32(values[2]);
            GuideRepository guideRepository = new();
            Guide = guideRepository.GetById(Convert.ToInt32(values[2]));
            foreach (string tour in values[3].Split(","))
            {
                int tourId = Convert.ToInt32(tour);
                TourIds.Add(tourId);
            }
            ContentNotification = values[4];
            DateSentNotification = DateTime.ParseExact(values[5], "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            Seen = Convert.ToBoolean(values[6]);
        }
    }
}
