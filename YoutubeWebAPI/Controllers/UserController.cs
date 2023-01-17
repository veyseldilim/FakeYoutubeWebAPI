using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using YoutubeWeb.Domain.Request.User;
using YoutubeWeb.Domain.Request.User.Validator;
using YoutubeWeb.Domain.Services;
using YoutubeWebAPI.Filters;

namespace YoutubeWebAPI.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var users = await _userService.GetAllUsers();

            return Ok(users);
        }

        [HttpGet("{id:guid}")]
        [UserExists]
        public async Task<IActionResult> GetById(Guid id)
        {
            var newUserRequest = new GetUserRequest() { Id = id};

            var user = await _userService.GetUserById(newUserRequest);

         

            return Ok(user);
        }

        
        
        [HttpGet("{id:guid}/comments")]
        [UserExists]
        public async Task<IActionResult> GetCommentsById(Guid id)
        {
            
            var newUserRequest = new GetUserRequest() { Id = id };

            var user = await _userService.GetUserComments(newUserRequest);

           

            return Ok(user);
        }

        [HttpGet("{id:guid}/posts")]
        [UserExists]
        public async Task<IActionResult> GetPostsById(Guid id)
        {
            var newUserRequest = new GetUserRequest() { Id = id };

            var user = await _userService.GetUserPosts(newUserRequest);

          

            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Post(AddUserRequest addUserRequest)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ModelState);
            }

            
            var result = await _userService.AddUser(addUserRequest);

            return CreatedAtAction(nameof(GetById), new { Id = result.Id}, null);

        }


        [HttpPut("{id:guid}")]
        [UserExists]
        public async Task<IActionResult> Put(Guid id, 
             EditUserRequest editUserRequest)
        {
           
            var result = await _userService.EditUser(editUserRequest);

            return Ok(result);
           

           
        }

    }
}
