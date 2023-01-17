using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YoutubeWeb.Domain.Request.Comment;
using YoutubeWeb.Domain.Request.Post;
using YoutubeWeb.Domain.Services;
using YoutubeWebAPI.Filters;

namespace YoutubeWebAPI.Controllers
{
    [Route("api/comments")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllComments()
        {
            var comments = await _commentService.GetAllComments();

            return Ok(comments);
        }


        [HttpPost]
        public async Task<IActionResult> Post(AddCommentRequest addCommentRequest)
        {

            var result = await _commentService.AddComment(addCommentRequest);

            return Ok(result);

        }

        [HttpPut("{id:guid}")] 
        public async Task<IActionResult> Put(Guid id,
             EditCommentRequest editCommentRequest)
        {
            var comment = await _commentService.GetCommentById(new GetCommentRequest { Id = id });

            if(comment == null)
            {
                return NotFound();
            }

            var result = await _commentService.EditComment(editCommentRequest);

            return Ok(result);

        }
    }
}
