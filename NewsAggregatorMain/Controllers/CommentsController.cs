using AutoMapper;
using Contracts.ServicesInterfacaces;
using Entities.DataTransferObject;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsAggregatorMain.Controllers
{
    public class CommentsController : Controller
    {
        private readonly ICommentService _commentService;
        private readonly IMapper _mapper;

        public CommentsController(ICommentService commentService, IMapper mapper)
        {
            _commentService = commentService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> List(Guid newsId)
        {
            var commentsListEnt = await _commentService.FindAllCommentsForNews(newsId);
            var commentsListDto = _mapper.Map<IEnumerable<CommentDto>>(commentsListEnt);

            return View(commentsListDto);
        }
    }
}
