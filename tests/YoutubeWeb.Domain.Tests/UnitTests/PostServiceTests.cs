using Newtonsoft.Json;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using YoutubeWeb.Data.Repositories;
using YoutubeWeb.Domain.Entities;
using YoutubeWeb.Domain.Mappers;
using YoutubeWeb.Domain.Request.Post;
using YoutubeWeb.Domain.Services;
using YoutubeWeb.Fixtures;
using Microsoft.EntityFrameworkCore;

namespace YoutubeWeb.Domain.Tests.UnitTests
{
    public class PostServiceTests : IDisposable
    {

        private readonly YoutubeWebContextFactory _youtubeWebContextFactory;
        private readonly CommentRepository _commentRepository;
        private readonly PostRepository _postRepository;
        private readonly UserRepository _userRepository;

        private readonly UserService _userService;
        private readonly PostService _postService;
        private readonly CommentService _commentService;

        private readonly IMapper _mapper;




        public PostServiceTests()
        {
            _youtubeWebContextFactory = new();

            _commentRepository = new (_youtubeWebContextFactory.ContextInstance);
            _postRepository = new (_youtubeWebContextFactory.ContextInstance);
            _userRepository = new (_youtubeWebContextFactory.ContextInstance);

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
        public async Task get_all_posts_should_return_right_data()
        {
            

            var posts = await _postService.GetAllPosts();

            posts.ShouldNotBeNull();
            posts.Count().ShouldBe(3);

        }

        [Theory]
        [InlineData("768f4a60-56aa-4cb4-9ae2-871ff71d75f6")]
        public async Task get_post_by_id_should_return_right_data(string guid)
        {
            

            GetPostRequest getPostRequest = new GetPostRequest()
            {
                Id = new Guid(guid)
            };

            var post = await _postService.GetPostById(getPostRequest);


            post.ShouldNotBeNull();
            post.Id.ShouldBe(getPostRequest.Id);
            post.UserId.ShouldBe(new Guid("ad93ac22-14db-4c1b-9133-85ddb60f026d"));


        }


        [Theory]
        [InlineData("6565a0b3-9812-499c-83f9-c52fc1eaf1e6")]
        public async Task get_post_by_id_should_not_return_data(string guid)
        {
            

            GetPostRequest getPostRequest = new GetPostRequest()
            {
                Id = new Guid(guid)
            };

            var post = await _postService.GetPostById(getPostRequest);

            post.ShouldBeNull();


        }


        [Fact]
        public async Task get_post_should_throw_null_exception()
        {
            

            var post = await _postService.GetPostById(null)
                             .ShouldThrowAsync<ArgumentNullException>();

            
        }

        [Theory]
        [InlineData(@"
        { 
        ""Title"": ""PostTitleTest4"",
        ""UserId"": ""ad93ac22-14db-4c1b-9133-85ddb60f026d"",
        ""Body"" : ""PostBodyTest4""
          }")]
       
        public async Task add_post_should_add_data(string addPostRequestJson)
        {
            AddPostRequest addPostRequest = JsonConvert.DeserializeObject<AddPostRequest>
                                            (addPostRequestJson)!;

            

            var result = await _postService.AddPost(addPostRequest);

            result.ShouldNotBeNull();
            result.Body.ShouldBe(addPostRequest.Body);

            var posts = await _postService.GetAllPosts();

            posts.Count().ShouldBe(4);

        }

        [Fact]
        public async Task add_post_should_throw_null_exception()
        {

            var result = await _postService.AddPost(null).ShouldThrowAsync<ArgumentNullException>();
        }

        

        [Theory]
        [InlineData(@"
        { 
        ""Title"": null,
        ""UserId"": ""ad93ac22-14db-4c1b-9133-85ddb60f026d"",
        ""Body"" : ""PostBodyTest4""
          }")]
        public async Task addPost_should_throw_validationError(string addPostRequestJson)
        {
            AddPostRequest addPostRequest = JsonConvert.DeserializeObject<AddPostRequest>
                                            (addPostRequestJson)!;

            var result = await _postService.AddPost(addPostRequest).ShouldThrowAsync<DbUpdateException>();

        }

        [Theory]
        [InlineData(@"
        { 
        ""Title"": ""PostTitleTest4"",
        ""UserId"": ""ad93ac22-14db-4c1b-9133-85ddb60f026d"",
        ""Body"" : null
          }")]
        public async Task addPost_should_not_throw_validationError_when_bodyMissing
            (string addPostRequestJson)
        {
            AddPostRequest addPostRequest = JsonConvert.DeserializeObject<AddPostRequest>
                                            (addPostRequestJson)!;

            var result = await _postService.AddPost(addPostRequest);

            result.ShouldNotBeNull();
            result.Title.ShouldBe(addPostRequest.Title);
            result.Body.ShouldBe(null);

        }


        [Theory]
        [InlineData(@"
        { 
        ""Title"": ""PostTitleTest4"",
        ""Id"": ""aee0b350-1a6f-40ed-9707-8cf6fa3d7e4e"",
        ""Body"" : null
          }")]
        public async Task editPost_should_update_post
            (string editPostRequestJson)
        {
            EditPostRequest editPostRequest = JsonConvert.DeserializeObject<EditPostRequest>
                                            (editPostRequestJson)!;

            var result = await _postService.EditPost(editPostRequest);

            result.ShouldNotBeNull();
            result.Title.ShouldBe(editPostRequest.Title);
            result.Body.ShouldBe(editPostRequest.Body);

        }




    }
}
