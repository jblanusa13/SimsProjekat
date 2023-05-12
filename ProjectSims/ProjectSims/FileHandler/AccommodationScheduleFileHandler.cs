using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectSims.Domain.Model;
using ProjectSims.Serializer;

namespace ProjectSims.FileHandler
{
    public class AccommodationScheduleFileHandler
    {
        private const string FilePath = "../../../Resources/Data/accommodationSchedule.csv";

        private readonly Serializer<AccommodationSchedule> serializer;

        public AccommodationScheduleFileHandler()
        {
            serializer = new Serializer<AccommodationSchedule>();
        }

        public List<AccommodationSchedule> Load()
        {
            return serializer.FromCSV(FilePath);
        }

        public void Save(List<AccommodationSchedule> accommodationSchedules)
        {
            serializer.ToCSV(FilePath, accommodationSchedules);
        }
    }
}
