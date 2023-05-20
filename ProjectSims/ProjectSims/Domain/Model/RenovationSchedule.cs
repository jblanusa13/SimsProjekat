using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using ProjectSims.Repository;
using ProjectSims.Serializer;

namespace ProjectSims.Domain.Model
{
    public class RenovationSchedule : ISerializable
    {
        public int Id { get; set; }
        public DateRanges DateRange { get; set; }
        public string Description { get; set; } 
        public int AccommodationId { get; set; }
        public Accommodation Accommodation { get; set; }

        public RenovationSchedule() { }

        public RenovationSchedule(int id, DateRanges dateRange, string description, int accomodationId, Accommodation accommodation)
        {
            Id = id;
            DateRange = dateRange;
            Description = description;
            AccommodationId = accomodationId;
            Accommodation = accommodation;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            DateRange = new DateRanges(DateOnly.ParseExact(values[1].Split("-")[0], "dd.MM.yyyy"), DateOnly.ParseExact(values[1].Split("-")[1], "dd.MM.yyyy"));
            Description = Convert.ToString(values[2]);
            AccommodationId = Convert.ToInt32(values[3]);
            InitializeData();
        }

        public string[] ToCSV()
        {
            string[] csvValues = {
                Id.ToString(),
                DateRange.CheckIn.ToString("dd.MM.yyyy") + "-" + DateRange.CheckOut.ToString("dd.MM.yyyy"),
                Description.ToString(),
                AccommodationId.ToString()
            };
            return csvValues;
        }

        private void InitializeData()
        {
            AccommodationRepository accommodationRepository = new AccommodationRepository();
            Accommodation = accommodationRepository.GetById(AccommodationId);
        }
    }
}
