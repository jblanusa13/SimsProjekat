using ProjectSims.Domain.Model;
using ProjectSims.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSims.FileHandler
{
    class QuestionsFileHandler
    {
        private const string FilePath = "../../../Resources/Data/questions.csv";


        private Serializer<Questions> _serializer;

        private List<Questions> questions;

        public QuestionsFileHandler()
        {
            _serializer = new Serializer<Questions>();
            questions = _serializer.FromCSV(FilePath);
        }

        public List<Questions> Load()
        {
            return _serializer.FromCSV(FilePath);
        }

        public void Save(List<Questions> questions)
        {
            _serializer.ToCSV(FilePath, questions);
        }
        
    }
}
