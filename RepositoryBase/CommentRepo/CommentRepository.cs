using Contracts.RepositoryInterfaces;
using Entities.Entity.NewsEnt;
using Repositories.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.CommentRepo
{
    public class CommentRepository : RepositoryBases<Comment>, ICommentRepository
    {
        public CommentRepository(NewsDataContext newsDataContext)
          : base(newsDataContext)
        {
        }
    }
}
