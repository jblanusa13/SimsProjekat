using ProjectSims.Domain.Model;
using ProjectSims.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSims.FileHandler
{
    public class RenovationScheduleFileHandler
    {
        private const string FilePath = "../../../Resources/Data/renovation.csv";

        private readonly Serializer<RenovationSchedule> _serializer;

        public RenovationScheduleFileHandler()
        {
            _serializer = new Serializer<RenovationSchedule>();
        }

        public List<RenovationSchedule> Load()
        {
            return _serializer.FromCSV(FilePath);
        }

        public void Save(List<RenovationSchedule> renovation)
        {
            _serializer.ToCSV(FilePath, renovation);
        }

    }
}
