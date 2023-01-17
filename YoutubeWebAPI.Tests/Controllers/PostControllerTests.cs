using Newtonsoft.Json;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using YoutubeWeb.Domain.Entities;
using YoutubeWeb.Domain.Request.User;
using YoutubeWeb.Fixtures;
using YoutubeWeb.Domain.Response;
using YoutubeWeb.Domain.Request.Post;
using Azure;

namespace YoutubeWebAPI.Tests.Controllers
{
    public class PostControllerTests
    {
        private readonly InMemoryApplicationFactory _factory;
        private readonly HttpClient _client;

        const string BaseUrl = "api/posts";

        public PostControllerTests()
        {
            _factory = new InMemoryApplicationFactory();
            _client = _factory.CreateClient();
        }

        [Theory]
        [InlineData(BaseUrl)]
        public async Task getAllPosts_should_return_success(string url)
        {

            var response = await _client.GetAsync(url);

            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            var responseEntity = JsonConvert.DeserializeObject<PostResponse[]>(responseContent);

            responseEntity.ShouldNotBeNull();
            responseEntity.Count().ShouldBe(3);

        }

        [Fact]
        public async Task get_by_id_should_return_post_data()
        {
            const string id = "768f4a60-56aa-4cb4-9ae2-871ff71d75f6";

            var response = await _client.GetAsync($"{BaseUrl}/{id}");
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            var responseEntity = JsonConvert.DeserializeObject<PostResponse>(responseContent);

            responseEntity.ShouldNotBeNull();
            responseEntity.Body.ShouldBe("PostBodyTest2");
            responseEntity.Title.ShouldBe("PostTitleTest2");
            responseEntity.Comments.ToList().Count().ShouldBe(4);
        }

        
        [Fact]
        public async Task get_by_nonexistent_postId_should_return_404()
        {
            const string id = "99826a80-e2e7-4919-af09-ae557bee24e5";

            var response = await _client.GetAsync($"{BaseUrl}/{id}");

            response.StatusCode.ShouldBe(HttpStatusCode.NotFound);

        }

        [Fact]
        public async Task addPost_should_create_new_post()
        {
            AddPostRequest addPostRequest = new()
            {
                Title = "Title",
                Body = "Body",
                UserId = Guid.NewGuid(),
            };

            var result = await _client.PostAsJsonAsync(BaseUrl, addPostRequest);

            result.EnsureSuccessStatusCode();
            result.Headers.Location.ShouldNotBeNull();
            Console.WriteLine(result.Headers.Location.ToString());

        }


        [Fact]
        public async Task addPost_shouldNot_create_new_post()
        {
            AddPostRequest addPostRequest = new()
            {
                Title = "",
                Body = "Body",
                UserId = Guid.NewGuid(),
            };

            var result = await _client.PostAsJsonAsync(BaseUrl, addPostRequest);
            
            
            result.IsSuccessStatusCode.ShouldBeFalse();
            result.StatusCode.ShouldBe(HttpStatusCode.BadRequest);

        }

        

        [Fact]
        public async Task editPost_should_edit()
        {
            EditPostRequest editPostRequest = new()
            {
                Id = new Guid("bef114ce-19ba-46b3-b3c6-e8cd5db653ad"),
                Body = "EditPostTest4",
                Title = "Title"
            };

            var result = await _client.PutAsJsonAsync($"{BaseUrl}/{editPostRequest.Id}",
                editPostRequest);

            result.EnsureSuccessStatusCode();
            result.StatusCode.ShouldBe(HttpStatusCode.OK);

        }

        [Fact]
        public async Task editPost_shouldNot_edit_titleLengthSmallerThan3()
        {
            EditPostRequest editPostRequest = new()
            {
                Id = new Guid("bef114ce-19ba-46b3-b3c6-e8cd5db653ad"),
                Body = "EditPostTest4",
                Title = "ti"
            };


            var result = await _client
                .PutAsJsonAsync($"{BaseUrl}/{editPostRequest.Id}"
                , editPostRequest);

            result.StatusCode.ShouldBe(HttpStatusCode.BadRequest);

        }

        [Fact]
        public async Task get_post_comments_by_id()
        {
            GetPostRequest getUserRequest = new()
            {
                Id = new Guid("bef114ce-19ba-46b3-b3c6-e8cd5db653ad")
            };

            var response = await _client.GetAsync(
                $"{BaseUrl}/{getUserRequest.Id}/comments");


            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            var responseEntity = JsonConvert.DeserializeObject<CommentResponse[]>(responseContent);

            responseEntity.Count().ShouldBe(0);

        }

    }


}
