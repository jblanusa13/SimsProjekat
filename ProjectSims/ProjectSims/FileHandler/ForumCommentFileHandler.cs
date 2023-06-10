using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectSims.Domain.Model;
using ProjectSims.Serializer;

namespace ProjectSims.FileHandler
{
    public class ForumCommentFileHandler
    {
        private const string FilePath = "../../../Resources/Data/forumComment.csv";


        private Serializer<ForumComment> _serializer;
        public ForumCommentFileHandler()
        {
            _serializer = new Serializer<ForumComment>();
        }
        public List<ForumComment> Load()
        {
            return _serializer.FromCSV(FilePath);
        }

        public void Save(List<ForumComment> comment)
        {
            _serializer.ToCSV(FilePath, comment);
        }
    }
}
