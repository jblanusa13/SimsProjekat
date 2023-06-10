using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectSims.Domain.Model;
using ProjectSims.Observer;

namespace ProjectSims.Domain.RepositoryInterface
{
    public interface IForumCommentRepository : IGenericRepository<ForumComment, int>, ISubject
    {
    }
}
