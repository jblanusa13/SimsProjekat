using ProjectSims.Domain.Model;
using ProjectSims.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSims.FileHandler
{
    class GuideScheduleFileHandler
    {
        private const string FilePath = "../../../Resources/Data/guideSchedule.csv";


        private Serializer<GuideSchedule> serializer;

        public GuideScheduleFileHandler()
        {
            serializer = new Serializer<GuideSchedule>();
        }

        public List<GuideSchedule> Load()
        {
            return serializer.FromCSV(FilePath);
        }

        public void Save(List<GuideSchedule> guideSchedules)
        {
            serializer.ToCSV(FilePath, guideSchedules);
        }
    }
}
