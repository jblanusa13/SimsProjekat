using ProjectSims.Domain.Model;
using ProjectSims.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSims.FileHandler
{
    class OwnerFileHandler
    {
        private const string FilePath = "../../../Resources/Data/owners.csv";

        private readonly Serializer<Owner> _serializer;

        public OwnerFileHandler()
        {
            _serializer = new Serializer<Owner>();
        }

        public List<Owner> Load()
        {
            return _serializer.FromCSV(FilePath);
        }

        public void Save(List<Owner> owners)
        {
            _serializer.ToCSV(FilePath, owners);
        }

        public Owner GetByUserId(int id)
        {
            List<Owner> owners = _serializer.FromCSV(FilePath);
            return owners.FirstOrDefault(o => o.UserId == id);
        }
    }
}
