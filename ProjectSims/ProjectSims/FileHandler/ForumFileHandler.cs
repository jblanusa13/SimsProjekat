using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectSims.Domain.Model;
using ProjectSims.Serializer;

namespace ProjectSims.FileHandler
{
    public class ForumFileHandler
    {
        private const string FilePath = "../../../Resources/Data/forum.csv";


        private Serializer<Forum> _serializer;
        public ForumFileHandler()
        {
            _serializer = new Serializer<Forum>();
        }
        public List<Forum> Load()
        {
            return _serializer.FromCSV(FilePath);
        }

        public void Save(List<Forum> forum)
        {
            _serializer.ToCSV(FilePath, forum);
        }
    }
}
