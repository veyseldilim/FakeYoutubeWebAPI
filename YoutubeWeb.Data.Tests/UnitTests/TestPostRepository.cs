using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeWeb.Data.Repositories;
using YoutubeWeb.Domain.Entities;

namespace YoutubeWeb.Data.Tests.UnitTests
{
    public class TestPostRepository
    {
        [Fact]
        public async Task should_get_posts()
        {
            var options = new DbContextOptionsBuilder<YoutubeContext>()
                         .UseInMemoryDatabase(databaseName: "should_get_posts_data")
                         .Options;

            await using (var context = new TestYoutubeContext(options))
            {
                context.Database.EnsureCreated();
                var sut = new PostRepository(context);
                var result = await sut.GetAsync();

                result.ShouldNotBeNull();
                result.Count().ShouldBe(3);



            }

        }

        [Theory]
        [InlineData("d43b3a4a-4120-46b6-abcc-47c0ca8a8b1e")]
        public async Task should_return_null_with_post_id_not_present(string guid)
        {
            var options = new DbContextOptionsBuilder<YoutubeContext>()
                .UseInMemoryDatabase(databaseName:
                "should_return_null_with_post_id_not_present")
                .Options;

            await using (var context = new TestYoutubeContext(options))
            {
                context.Database.EnsureCreated();
                var sut = new PostRepository(context);
                var result = await sut.GetById(new Guid(guid));
                result.ShouldBeNull();
            }

        }

        [Theory]
        [InlineData("bef114ce-19ba-46b3-b3c6-e8cd5db653ad")]
        public async Task should_return_post_by_id(string guid)
        {
            var options = new DbContextOptionsBuilder<YoutubeContext>()
                .UseInMemoryDatabase(databaseName:
                "should_return_post_by_id_present")
                .Options;

            await using (var context = new TestYoutubeContext(options))
            {
                context.Database.EnsureCreated();
                var sut = new PostRepository(context);
                var result = await sut.GetById(new Guid(guid));


                result.ShouldNotBeNull();
                result.Id.ShouldBe(new Guid(guid));
                result.Title.ShouldBe("PostTitleTest3");
                result.Body.ShouldBe("PostBodyTest3");
                result.UserId.ShouldBe(new Guid("ad93ac22-14db-4c1b-9133-85ddb60f026d"));
                result.PostComments?.Count.ShouldBe(0);
            }
        }

        [Theory]
        [InlineData("768f4a60-56aa-4cb4-9ae2-871ff71d75f6")]
        public async Task should_return_post_by_id_with_comments(string guid)
        {
            var options = new DbContextOptionsBuilder<YoutubeContext>()
                .UseInMemoryDatabase(databaseName:
                "should_return_post_by_id_with_comments")
                .Options;

            await using (var context = new TestYoutubeContext(options))
            {
                context.Database.EnsureCreated();
                var sut = new PostRepository(context);
                var result = await sut.GetById(new Guid(guid));


                result.ShouldNotBeNull();
                result.Id.ShouldBe(new Guid(guid));
                result.Title.ShouldBe("PostTitleTest2");
                result.Body.ShouldBe("PostBodyTest2");
                result.UserId.ShouldBe(new Guid("ad93ac22-14db-4c1b-9133-85ddb60f026d"));
                result.PostComments?.Count.ShouldBe(4);
                result.PostComments?.ToArray()[3].Id.ShouldBe(new Guid("99826a80-e2e7-4919-af09-ae557bee24e5"));
            }
        }

        

        [Theory]
        [InlineData(@"
            { 
            ""Id"": ""ad215ca6-dd01-4f43-b9fe-f174d2176f19"", 
            ""Title"": ""PostTitleTest4"",
            ""Body"": ""PostBodyTest4"",
            ""UserId"": ""f3f9f5a2-1b8e-4c12-a35b-6e4c511bd737"",
            ""User"": null,   
            ""PostComments"": null  }")]
        public async Task should_add_new_post(string jsonPost)
        {
            var post = JsonConvert.DeserializeObject<Post>(jsonPost);

            post.ShouldNotBeNull();

            var options = new DbContextOptionsBuilder<YoutubeContext>()
               .UseInMemoryDatabase(databaseName:
               "should_add_new_post")
               .Options;

            await using (var context = new TestYoutubeContext(options))
            {
                context.Database.EnsureCreated();
                var sut = new PostRepository(context);

                var result = sut.Add(post);

                await sut.UnitOfWork.SaveChangesAsync();

                var addedPost = await sut.GetById(new Guid($"{post.Id}"));
                
                addedPost.ShouldNotBeNull();
                addedPost.Id.ShouldBe(post.Id);
                addedPost.Body.ShouldBe(post.Body);
                addedPost.PostComments.Count.ShouldBe(0);
                addedPost.User.Name.ShouldBe("TestUser1");

                var newSut = new UserRepository(context);
                var user = await newSut.GetById(new Guid("f3f9f5a2-1b8e-4c12-a35b-6e4c511bd737"));
                //addedPost.User.Posts.ToList().Count.ShouldBe(2);

                user.Posts.ToList().Count().ShouldBe(2);
                

            }
        }

        
        [Theory]
        [InlineData(@"
            { 
            ""Id"": ""bef114ce-19ba-46b3-b3c6-e8cd5db653ad"", 
            ""Title"": ""PostTitleTest3"",
            ""Body"": ""PostBodyTest3"",
            ""UserId"": ""ad93ac22-14db-4c1b-9133-85ddb60f026d"",
            ""User"": null,   
            ""PostComments"": null  }")]
        public async Task should_update_post(string jsonPost)
        {
            var post = JsonConvert.DeserializeObject<Post>(jsonPost);
            post.Body = "UpdatedBody";

            var options = new DbContextOptionsBuilder<YoutubeContext>()
                .UseInMemoryDatabase(databaseName: "should_update_post")
                .Options;

            await using var context = new TestYoutubeContext(options);
            context.Database.EnsureCreated();
            var sut = new PostRepository(context);

            sut.Update(post);
            await sut.UnitOfWork.SaveEntitiesAsync();
            context.Posts
                .FirstOrDefault(x => x.Id == post.Id)?.Body.ShouldBe("UpdatedBody");


        }

        

        
    }
}
