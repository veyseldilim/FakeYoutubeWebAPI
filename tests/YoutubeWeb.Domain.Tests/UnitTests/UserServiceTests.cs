using Newtonsoft.Json;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeWeb.Data.Repositories;
using YoutubeWeb.Domain.Mappers;
using YoutubeWeb.Domain.Request.Post;
using YoutubeWeb.Domain.Request.User;
using YoutubeWeb.Domain.Services;
using YoutubeWeb.Fixtures;
using Microsoft.EntityFrameworkCore;
using YoutubeWeb.Domain.Request.User.Validator;

namespace YoutubeWeb.Domain.Tests.UnitTests
{
    public class UserServiceTests : IDisposable
    {
        private readonly YoutubeWebContextFactory _youtubeWebContextFactory;

        private readonly CommentRepository _commentRepository;
        private readonly PostRepository _postRepository;
        private readonly UserRepository _userRepository;

        private readonly UserService _userService;
        private readonly PostService _postService;
        private readonly CommentService _commentService;

        private readonly IMapper _mapper;

       



        public UserServiceTests()
        {
            _youtubeWebContextFactory = new();

            _commentRepository = new(_youtubeWebContextFactory.ContextInstance);
            _postRepository = new(_youtubeWebContextFactory.ContextInstance);
            _userRepository = new(_youtubeWebContextFactory.ContextInstance);

            _mapper = _youtubeWebContextFactory.Mapper;

            _postService = new PostService(_postRepository, _commentRepository, _mapper);
            _userService = new UserService(_userRepository, _commentRepository, _postRepository, _mapper);
            _commentService = new CommentService(_commentRepository, _mapper);

            


        }

        public void Dispose()
        {
            _youtubeWebContextFactory.ContextInstance.Dispose();
        }

        [Fact]
        public async Task getAllUsers_should_return_allUsers()
        {
            var users = await _userService.GetAllUsers();

            users.ShouldNotBeNull();
            users.Count().ShouldBe(2);
        }


        [Theory]
        [InlineData("f3f9f5a2-1b8e-4c12-a35b-6e4c511bd737")]
        public async Task getUserById_should_return_right_user(string guid)
        {
            GetUserRequest getUserRequest = new() { Id = new Guid(guid) };

            var user = await _userService.GetUserById(getUserRequest);

            user.ShouldNotBeNull();
            user.Name.ShouldBe("TestUser1");
            user.Comments.ShouldNotBeNull();
            user.Comments.Count().ShouldBe(4);
            user.Posts.Count().ShouldBe(1);


        }

        [Theory]
        [InlineData("c70441c5-28db-4859-b5eb-282c7e5135f2")]
        public async Task getUserById_should_return_null(string guid)
        {
            GetUserRequest getUserRequest = new() { Id = new Guid(guid) };

            var user = await _userService.GetUserById(getUserRequest);

            user.ShouldBeNull();

        }


        [Theory]
        [InlineData(@"
        { 
        ""Name"" : ""UserNameTest3""
          }")]
        public async Task addUser_should_add_user(string addUserJson)
        {
            AddUserRequest addUser = JsonConvert.DeserializeObject<AddUserRequest>
                                            (addUserJson);

            var result = await _userService.AddUser(addUser);

            result.ShouldNotBeNull();
            result.Name.ShouldBe("UserNameTest3");
            result.Comments.ShouldBeNull();
            result.Posts.ShouldBeNull();

        }

        [Theory]
        [InlineData(@"
        { 
        ""Name"" : null
          }")]
        public async Task addUser_shouldNot_add_user(string addUserJson)
        {
            AddUserRequest addUser = JsonConvert.DeserializeObject<AddUserRequest>
                                            (addUserJson);

            var result = await _userService.AddUser(addUser).ShouldThrowAsync<DbUpdateException>();

            
        }

        [Theory]
        [InlineData(@"
        { 
        ""Id"" : ""ad93ac22-14db-4c1b-9133-85ddb60f026d"",
        ""Name"" : ""UserNameTest3""
          }")]
        public async Task editUser_should_edit_user(string editUserRequestJson)
        {
            EditUserRequest toBeEditedUser = JsonConvert.DeserializeObject<EditUserRequest>
                                            (editUserRequestJson);

            var result = await _userService.EditUser(toBeEditedUser);

            result.ShouldNotBeNull();
            result.Name.ShouldBe("UserNameTest3");
            

        }


        [Theory]
        [InlineData("ad93ac22-14db-4c1b-9133-85ddb60f026d")]
        public async Task getUserComments_should_return_right_data(string id)
        {
            GetUserRequest userReq = new() { Id = new Guid(id) };

            var result = await _userService.GetUserComments(userReq);

            result.ShouldNotBeNull();
            result.Count().ShouldBe(1);
        }

        [Theory]
        [InlineData("ad93ac22-14db-4c1b-9133-85ddb60f026d")]
        public async Task getUserPosts_should_return_right_data(string id)
        {
            GetUserRequest userReq = new() { Id = new Guid(id) };

            var result = await _userService.GetUserPosts(userReq);

            result.ShouldNotBeNull();
            result.Count().ShouldBe(2);
        }


    }
}
