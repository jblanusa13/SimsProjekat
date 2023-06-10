using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectSims.Domain.Model;
using ProjectSims.Serializer;

namespace ProjectSims.FileHandler
{
    public class OwnerCommentFileHandler
    {
        private const string FilePath = "../../../Resources/Data/ownerComment.csv";


        private Serializer<OwnerComment> _serializer;
        public OwnerCommentFileHandler()
        {
            _serializer = new Serializer<OwnerComment>();
        }
        public List<OwnerComment> Load()
        {
            return _serializer.FromCSV(FilePath);
        }

        public void Save(List<OwnerComment> ownerComment)
        {
            _serializer.ToCSV(FilePath, ownerComment);
        }
    }
}
