using ProjectSims.Model;
using ProjectSims.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSims.FileHandler
{
    class GuideFileHandler
    {
        private const string FilePath = "../../../Resources/Data/guide.csv";


        private Serializer<Guide> _serializer;

        private List<Guide> guides;

        public GuideFileHandler()
        {
            _serializer = new Serializer<Guide>();
            guides = _serializer.FromCSV(FilePath);
        }

        public Guide GetByUserId(int id)
        {
            guides = _serializer.FromCSV(FilePath);
            return guides.FirstOrDefault(g => g.UserId == id);
        }

        public List<Guide> Load()
        {
            return _serializer.FromCSV(FilePath);
        }

        public void Save(List<Guide> guides)
        {
            _serializer.ToCSV(FilePath, guides);
        }
    }
}
