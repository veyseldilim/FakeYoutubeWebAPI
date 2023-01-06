using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace YoutubeWebAPI.Controllers
{
    [Route("api/posts")]
    [ApiController]
    public class PostController : ControllerBase
    {
        public PostController()
        {

        }

        [HttpGet]
        public  IActionResult GetPosts()
        {
            return Ok();
        }
    }
}
