using AutoMapper;
using Contracts.ServicesInterfacaces;
using Contracts.UnitOfWorkInterface;
using Entities.DataTransferObject;
using Entities.Entity.NewsEnt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NewsAggregatorMain.Controllers
{
    public class CommentsController : Controller
    {
        private readonly ICommentService _commentService;
        private readonly IUserService _userService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CommentsController(ICommentService commentService, IMapper mapper, IUserService userService, IUnitOfWork unitOfWork)
        {
            _commentService = commentService;
            _mapper = mapper;
            _userService = userService;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> List(Guid newsId)
        {
            var commentsListEnt = await _commentService.FindAllCommentsForNews(newsId);
            var commentsListDto = _mapper.Map<IEnumerable<CommentDto>>(commentsListEnt)
                .OrderBy(x => x.CreateDate);


            return View(new CreateCommentDto
            {
                NewsId = newsId,
                Comments = commentsListDto
            });
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCommentDto createComment)
        {
            var user = HttpContext.User.Claims.FirstOrDefault(c => c.Type.Equals(ClaimsIdentity.DefaultNameClaimType));
            var userEmail = user?.Value;
            var userId = (await _userService.GetUserByLogin(userEmail)).Id;

            var comment = new Comment
            {
                Id = Guid.NewGuid(),
                NewsId = createComment.NewsId,
                Text = createComment.CommentText,
                CreateDate = DateTime.Now,
                UserId = userId,
            };

            _unitOfWork.Comment.Add(comment);
            await _unitOfWork.SaveAsync();
            return Ok();
        }


        [HttpGet]
        public IActionResult InputCommentArea()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> DeleteComment(Guid commentId)
        {
            var comment = await _commentService.GetCommentById(commentId);
            _commentService.DeleteComment(comment);
            await _unitOfWork.SaveAsync();
            return Ok();
        }


    }
}
