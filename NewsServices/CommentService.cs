﻿using Contracts.ServicesInterfacaces;
using Contracts.UnitOfWorkInterface;
using Entities.Entity.NewsEnt;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services
{
    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CommentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Comment>> FindAllCommentsForNews(Guid newsId)
        {
            var comments = await _unitOfWork.Comment.GetByCondition(x => x.NewsId.Equals(newsId), false)
                .Include(x => x.User)
                .ToListAsync();
            return comments;
        }

        public void CreateComment(Comment comment)
        {
            _unitOfWork.Comment.Add(comment);
        }

        public void DeleteComment(Comment comment)
        {
            _unitOfWork.Comment.Remove(comment);
        }

        public async Task<Comment> GetCommentById(Guid commentID)
        {
            var comment = await  _unitOfWork.Comment.GetByCondition(x => x.Id.Equals(commentID), false).SingleOrDefaultAsync();
            return comment;
        }
    }
}
