using ProjectSims.Domain.Model;
using ProjectSims.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSims.FileHandler
{
    public class RenovationFileHandler
    {
        private const string FilePath = "../../../Resources/Data/renovation.csv";

        private readonly Serializer<Renovation> _serializer;

        public RenovationFileHandler()
        {
            _serializer = new Serializer<Renovation>();
        }

        public List<Renovation> Load()
        {
            return _serializer.FromCSV(FilePath);
        }

        public void Save(List<Renovation> renovation)
        {
            _serializer.ToCSV(FilePath, renovation);
        }

    }
}
