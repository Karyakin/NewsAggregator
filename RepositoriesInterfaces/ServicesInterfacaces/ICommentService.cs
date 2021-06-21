using Entities.Entity.NewsEnt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.ServicesInterfacaces
{
    public interface ICommentService
    {
        Task<IEnumerable<Comment>> FindAllCommentsForNews(Guid newsId);
        Task<Comment> GetCommentById(Guid commentID);
        void CreateComment(Comment comment);
        void DeleteComment(Comment comment);
    }
}
