using ProjectSims.Domain.Model;
using ProjectSims.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ProjectSims.FileHandler
{
    class KeyPointFileHandler
    {
        private const string FilePath = "../../../Resources/Data/keypoint.csv";


        private Serializer<KeyPoint> _serializer;

        public KeyPointFileHandler()
        {
            _serializer = new Serializer<KeyPoint>();
        }

        public List<KeyPoint> Load()
        {
            return _serializer.FromCSV(FilePath);
        }

        public void Save(List<KeyPoint> keyPoints)
        {
            _serializer.ToCSV(FilePath, keyPoints);
        }
    }
}
