using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Newtonsoft.Json;
using Shouldly;
using YoutubeWeb.Data.Repositories;
using YoutubeWeb.Domain.Entities;
using YoutubeWeb.Domain.Repositories;
using YoutubeWeb.Fixtures;

namespace YoutubeWeb.Data.Tests.UnitTests
{
    public class TestCommentRepository : IDisposable
    {
        private readonly YoutubeWebContextFactory _contextFactory;
        private readonly CommentRepository _sut;
        private readonly TestYoutubeContext _context;

        public TestCommentRepository()
        {
            _contextFactory = new();
            _context = _contextFactory.ContextInstance;
            _sut = new (_context);
        }

        public void Dispose() 
        {
            _contextFactory.ContextInstance.Dispose();
        }


        [Fact]
        public async Task should_get_dataAsync()
        {

            var result = await _sut.GetAsync();

            result.ShouldNotBeNull();
            result.Count().ShouldBe(5);



        }

        [Fact]
        public async Task should_returns_null_with_comment_id_not_present()
        {
           
            var result = await _sut.GetById(Guid.NewGuid());
            result.ShouldBeNull();
            
           
        }

        [Theory]
        [InlineData("164e134a-5d05-444a-9454-ea5c5edc82f0")]
        public async Task should_return_comment_by_id(string guid)
        {
            

            
            var result = await _sut.GetById(new Guid("164e134a-5d05-444a-9454-ea5c5edc82f0"));


            result.ShouldNotBeNull();
            result.Id.ShouldBe(new Guid("164e134a-5d05-444a-9454-ea5c5edc82f0"));
            result.Body.ShouldBe("CommentBodyTest2");
            result.PostId.ShouldBe(new Guid("768f4a60-56aa-4cb4-9ae2-871ff71d75f6"));
            result.UserId.ShouldBe(new Guid("f3f9f5a2-1b8e-4c12-a35b-6e4c511bd737"));

            
        }

        [Theory]
        [InlineData(@"
            { 
            ""Id"": ""ca34add4-53fe-4a4d-9ee9-4de81cc0bbfa"", 
            ""Body"": ""CommentBodyTest6"",
            ""UserId"": ""ad93ac22-14db-4c1b-9133-85ddb60f026d"",
            ""User"": null,   
            ""PostId"": ""bef114ce-19ba-46b3-b3c6-e8cd5db653ad"",
            ""Post"": null  }")]
        public async Task should_add_new_comment(string jsonComment)
        {
            var comment = JsonConvert.DeserializeObject<Comment>(jsonComment);

            comment.ShouldNotBeNull();

            
            var result = _sut.Add(comment);

            await _sut.UnitOfWork.SaveChangesAsync();

            var addedComment = await _sut.GetById(comment.Id);

            addedComment.ShouldNotBeNull();
            addedComment.Id.ShouldBe(comment.Id);
            addedComment.PostId.ShouldBe(comment.PostId);
            addedComment.UserId.ShouldBe(comment.UserId);
            addedComment.Body.ShouldBe(comment.Body);
    
        }

        [Theory]
        [InlineData(@"
            { 
            ""Id"": ""99826a80-e2e7-4919-af09-ae557bee24e5"", 
            ""Body"": ""CommentBodyTest6"",
            ""UserId"": ""f3f9f5a2-1b8e-4c12-a35b-6e4c511bd737"",
            ""User"": null,   
            ""PostId"": ""768f4a60-56aa-4cb4-9ae2-871ff71d75f6"",
            ""Post"": null  }")]
        public async Task should_update_item(string jsonComment)
        {
            var comment = JsonConvert.DeserializeObject<Comment>(jsonComment);
            comment.Body = "UpdatedBody";

            

            _sut.Update(comment);
            await _sut.UnitOfWork.SaveEntitiesAsync();
            _context.Comments
                .FirstOrDefault(x => x.Id == comment.Id)?.Body.ShouldBe("UpdatedBody");
                
                
        }


    }
}