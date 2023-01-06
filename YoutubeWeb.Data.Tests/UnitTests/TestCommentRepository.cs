using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Shouldly;
using YoutubeWeb.Data.Repositories;
using YoutubeWeb.Domain.Entities;
using YoutubeWeb.Domain.Repositories;

namespace YoutubeWeb.Data.Tests.UnitTests
{
    public class TestCommentRepository
    {
        [Fact]
        public async Task should_get_dataAsync()
        {
            var options = new DbContextOptionsBuilder<YoutubeContext>()
                         .UseInMemoryDatabase(databaseName: "should_get_comment_data")
                         .Options;

            await using (var context = new TestYoutubeContext(options))
            {
                context.Database.EnsureCreated();
                var sut = new CommentRepository(context);
                var result = await sut.GetAsync();

                result.ShouldNotBeNull();
                result.Count().ShouldBe(5);
 
            }
            
        }

        [Fact]
        public async Task should_returns_null_with_comment_id_not_present()
        {
            var options = new DbContextOptionsBuilder<YoutubeContext>()
                .UseInMemoryDatabase(databaseName:
                "should_returns_null_with_comment_id_not_present")
                .Options;

            await using (var context = new TestYoutubeContext(options))
            {
                context.Database.EnsureCreated();
                var sut = new CommentRepository(context);
                var result = await sut.GetById(Guid.NewGuid());
                result.ShouldBeNull();
            }
           
        }

        [Theory]
        [InlineData("164e134a-5d05-444a-9454-ea5c5edc82f0")]
        public async Task should_return_comment_by_id(string guid)
        {
            var options = new DbContextOptionsBuilder<YoutubeContext>()
                .UseInMemoryDatabase(databaseName:
                "should_returns_comment_with_id_present")
                .Options;

            await using (var context = new TestYoutubeContext(options))
            {
                context.Database.EnsureCreated();
                var sut = new CommentRepository(context);
                var result = await sut.GetById(new Guid("164e134a-5d05-444a-9454-ea5c5edc82f0"));


                result.ShouldNotBeNull();
                result.Id.ShouldBe(new Guid("164e134a-5d05-444a-9454-ea5c5edc82f0"));
                result.Body.ShouldBe("CommentBodyTest2");
                result.PostId.ShouldBe(new Guid("768f4a60-56aa-4cb4-9ae2-871ff71d75f6"));
                result.UserId.ShouldBe(new Guid("f3f9f5a2-1b8e-4c12-a35b-6e4c511bd737"));

            }
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

            var options = new DbContextOptionsBuilder<YoutubeContext>()
               .UseInMemoryDatabase(databaseName:
               "should_add_new_comment")
               .Options;

            await using (var context = new TestYoutubeContext(options))
            {
                context.Database.EnsureCreated();
                var sut = new CommentRepository(context);

                var result = sut.Add(comment);

                await sut.UnitOfWork.SaveChangesAsync();

                var addedComment = await sut.GetById(new Guid($"{comment.Id}"));

                addedComment.ShouldNotBeNull();
                addedComment.Id.ShouldBe(comment.Id);
                addedComment.PostId.ShouldBe(comment.PostId);
                addedComment.UserId.ShouldBe(comment.UserId);
                addedComment.Body.ShouldBe(comment.Body);
                addedComment.Post.Body.ShouldBe("PostBodyTest3");
                addedComment.User.Name.ShouldBe("TestUser2");


            }
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

            var options = new DbContextOptionsBuilder<YoutubeContext>()
                .UseInMemoryDatabase(databaseName: "should_update_comment")
                .Options;

            await using var context = new TestYoutubeContext(options);
            context.Database.EnsureCreated();
            var sut = new CommentRepository(context);

            sut.Update(comment);
            await sut.UnitOfWork.SaveEntitiesAsync();
            context.Comments
                .FirstOrDefault(x => x.Id == comment.Id)?.Body.ShouldBe("UpdatedBody");
                
                
        }


    }
}