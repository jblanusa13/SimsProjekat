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


        private Serializer<ForumComment> _serializer;
        public OwnerCommentFileHandler()
        {
            _serializer = new Serializer<ForumComment>();
        }
        public List<ForumComment> Load()
        {
            return _serializer.FromCSV(FilePath);
        }

        public void Save(List<ForumComment> ownerComment)
        {
            _serializer.ToCSV(FilePath, ownerComment);
        }
    }
}
