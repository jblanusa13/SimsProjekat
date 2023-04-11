using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectSims.Domain.Model;
using ProjectSims.Serializer;

namespace ProjectSims.FileHandler
{
    public class RequestFileHandler
    {
        private const string FilePath = "../../../Resources/Data/request.csv";

        private readonly Serializer<Request> _serializer;

        public RequestFileHandler()
        {
            _serializer = new Serializer<Request>();
        }

        public List<Request> Load()
        {
            return _serializer.FromCSV(FilePath);
        }

        public void Save(List<Request> request)
        {
            _serializer.ToCSV(FilePath, request);
        }
    }
}
