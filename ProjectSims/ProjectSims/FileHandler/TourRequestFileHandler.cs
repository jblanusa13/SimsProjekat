using ProjectSims.Domain.Model;
using ProjectSims.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSims.FileHandler
{
    class TourRequestFileHandler
    {
        private const string FilePath = "../../../Resources/Data/tourrequest.csv";

        private Serializer<TourRequest> _serializer;
        public TourRequestFileHandler()
        {
            _serializer = new Serializer<TourRequest>();
        }
        public List<TourRequest> Load()
        {
            return _serializer.FromCSV(FilePath);
        }
        public void Save(List<TourRequest> toursRequest)
        {
            _serializer.ToCSV(FilePath, toursRequest);
        }
    }
}
