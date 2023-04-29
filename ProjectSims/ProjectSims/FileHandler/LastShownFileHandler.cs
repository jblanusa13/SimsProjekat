using ProjectSims.Domain.Model;
using ProjectSims.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSims.FileHandler
{
    class LastShownFileHandler
    {
        private const string FilePath = "../../../Resources/Data/lastShown.csv";


        private Serializer<LastShown> _serializer;

        private List<LastShown> lastShownList;

        public LastShownFileHandler()
        {
            _serializer = new Serializer<LastShown>();
            lastShownList = _serializer.FromCSV(FilePath);
        }

        public List<LastShown> Load()
        {
            return _serializer.FromCSV(FilePath);
        }

        public void Save(List<LastShown> lastShownList)
        {
            _serializer.ToCSV(FilePath, lastShownList);
        }
    }
}
