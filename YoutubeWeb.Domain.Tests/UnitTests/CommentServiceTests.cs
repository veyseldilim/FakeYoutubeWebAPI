using Newtonsoft.Json;
using Shouldly;
using System.Net.Http.Json;
using YoutubeWeb.Data.Repositories;
using YoutubeWeb.Domain.Entities;
using YoutubeWeb.Domain.Mappers;
using YoutubeWeb.Domain.Request.Comment;
using YoutubeWeb.Domain.Request.Comment.Validator;
using YoutubeWeb.Domain.Request.Post;
using YoutubeWeb.Domain.Request.User;
using YoutubeWeb.Domain.Services;
using YoutubeWeb.Fixtures;

namespace YoutubeWeb.Domain.Tests.UnitTests
{
    public class CommentServiceTests : IDisposable
    {

        private readonly YoutubeWebContextFactory _youtubeWebContextFactory;

        private readonly CommentRepository _commentRepository;
        private readonly PostRepository _postRepository;
        private readonly UserRepository _userRepository;

        private readonly UserService _userService;
        private readonly PostService _postService;
        private readonly CommentService _commentService;

        private readonly IMapper _mapper;

       

        public CommentServiceTests()
        {
            _youtubeWebContextFactory = new YoutubeWebContextFactory();

            _commentRepository = new CommentRepository(_youtubeWebContextFactory.ContextInstance);
            _postRepository = new PostRepository(_youtubeWebContextFactory.ContextInstance);
            _userRepository = new UserRepository(_youtubeWebContextFactory.ContextInstance);

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
        public async Task get_all_comments_should_return_right_data()
        {

            var result = await _commentService.GetAllComments();

            result.ShouldNotBeNull();
            result.Count().ShouldBe(5);

            result.ElementAt(4).Id.ShouldBe(new Guid("99826a80-e2e7-4919-af09-ae557bee24e5"));

        }

        [Theory]
        [InlineData("99826a80-e2e7-4919-af09-ae557bee24e5")]
        public async Task get_commentbyid_should_return_right_data(string guid)
        {

            var result = await _commentService.GetCommentById
                (new GetCommentRequest { Id = new Guid($"{guid}")});

            result.ShouldNotBeNull();
            result.Id.ShouldBe(new Guid("99826a80-e2e7-4919-af09-ae557bee24e5"));
            result.PostId.ShouldBe(new Guid("768f4a60-56aa-4cb4-9ae2-871ff71d75f6"));
            result.UserId.ShouldBe(new Guid("f3f9f5a2-1b8e-4c12-a35b-6e4c511bd737"));

        }
        [Theory]
        [InlineData("99826a80-e2e7-4919-af09-ae557bee24e5")]
        public async Task get_commentbyid_should_return_false_data(string guid)
        {

            var result = await _commentService.GetCommentById
                (new GetCommentRequest { Id = new Guid($"{guid}") });

            result.ShouldNotBeNull();
            result.Id.ShouldBe(new Guid("99826a80-e2e7-4919-af09-ae557bee24e5"));
            result.PostId.ShouldNotBe(new Guid("768f4a60-56aa-4cb4-9ae2-871ff71d75f5"));
            result.UserId.ShouldBe(new Guid("f3f9f5a2-1b8e-4c12-a35b-6e4c511bd737"));

        }

        [Theory]
        [InlineData("8e6e8ac7-2ba2-44a5-a1bc-9e894427a105")]
        public async Task get_commentbyid_should_return_null_data(string guid)
        {
            var result = await _commentService.GetCommentById
                (new GetCommentRequest { Id = new Guid($"{guid}") });

            result.ShouldBeNull();

        }

        [Theory]
        [InlineData(@"
            { 
            ""Body"": ""AddCommentBodyTest6"",
            ""UserId"": ""f3f9f5a2-1b8e-4c12-a35b-6e4c511bd737"",
            ""User"": null,   
            ""PostId"": ""bef114ce-19ba-46b3-b3c6-e8cd5db653ad"",
            ""Post"": null  }"
        )]

        public async Task addComment_Should_Add_Data(string addCommentJson)
        {
            var commentRequest = JsonConvert.DeserializeObject<AddCommentRequest>(addCommentJson);

            var result =  await _commentService.AddComment(commentRequest);


            result.ShouldNotBeNull();
            result.Body.ShouldBe(commentRequest.Body);
            result.PostId.ShouldNotBeNull();
            result.PostId.ShouldBe(commentRequest.PostId);
            result.UserId.ShouldBe(commentRequest.UserId);



            GetPostRequest postRequest = new GetPostRequest()
            {
                Id = (Guid)result.PostId
            };

            var postComments = await _postService.GetPostComments(postRequest);
                

            postComments.ShouldNotBeNull();
            postComments.Count().ShouldBe(1);


            


            GetUserRequest userRequest = new GetUserRequest()
            {
                Id = (Guid)result.UserId
            };

            var userComments = await _userService.GetUserComments(userRequest);


            userComments.ShouldNotBeNull();
            userComments.Count().ShouldBe(5);


        }


        [Theory]
        [InlineData(@"
            { 
            ""Body"": ""EditCommentBodyTest6"",
            ""Id"": ""99826a80-e2e7-4919-af09-ae557bee24e5""
            }"
        )]

        public async Task should_edit_comment(string editCommentRequestJson)
        {
            EditCommentRequest editCommentRequest = JsonConvert.DeserializeObject
                                                    <EditCommentRequest>(editCommentRequestJson)!;

            var result = await _commentService.EditComment(editCommentRequest);

            result.ShouldNotBeNull();
            result.Id.ShouldBe(editCommentRequest.Id);
            result.Body.ShouldBe(editCommentRequest.Body);


        }

    }
}