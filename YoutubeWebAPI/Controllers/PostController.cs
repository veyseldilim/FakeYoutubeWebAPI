using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YoutubeWeb.Domain.Request.Post;
using YoutubeWeb.Domain.Request.User;
using YoutubeWeb.Domain.Services;
using YoutubeWebAPI.Filters;

namespace YoutubeWebAPI.Controllers
{
    [Route("api/posts")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPosts()
        {
            var posts = await _postService.GetAllPosts();

            return Ok(posts);
        }

        [HttpGet("{id:guid}")]
        [PostExists]
        public async Task<IActionResult> GetPostById(Guid id)
        {
            var postRequest = new GetPostRequest() { Id = id };

            var post = await _postService.GetPostById(postRequest);

            return Ok(post);
        }

        [HttpGet("{id:guid}/comments")]
        [PostExists]
        public async Task<IActionResult> GetPostCommentsById(Guid id)
        {

            var postRequest = new GetPostRequest() { Id = id };

            var postComments = await _postService.GetPostComments(postRequest);


            return Ok(postComments);
        }


        [HttpPost]
        public async Task<IActionResult> Post(AddPostRequest addPostRequest)
        {

            var result = await _postService.AddPost(addPostRequest);

            return CreatedAtAction(nameof(GetPostById), new { Id = result.Id }, null);

        }

        [HttpPut("{id:guid}")]
        [PostExists]
        public async Task<IActionResult> Put(Guid id,
             EditPostRequest editPostRequest)
        {
            Console.WriteLine("Put girdi mi");
            var result = await _postService.EditPost(editPostRequest);

            return Ok(result);

        }

    }
}
