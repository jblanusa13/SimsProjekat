using ProjectSims.Domain.Model;
using ProjectSims.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSims.FileHandler
{
    public class RequestForComplexTourFileHandler
    {
        private const string FilePath = "../../../Resources/Data/requestforcomplextour.csv";

        private Serializer<RequestForComplexTour> _serializer;
        public RequestForComplexTourFileHandler()
        {
            _serializer = new Serializer<RequestForComplexTour>();
        }
        public List<RequestForComplexTour> Load()
        {
            return _serializer.FromCSV(FilePath);
        }
        public void Save(List<RequestForComplexTour> toursRequest)
        {
            _serializer.ToCSV(FilePath, toursRequest);
        }
    }
}
