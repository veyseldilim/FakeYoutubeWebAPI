using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeWeb.Domain.Entities;
using YoutubeWeb.Fixtures;
using Shouldly;
using YoutubeWeb.Domain.Request.User;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Azure;

namespace YoutubeWebAPI.Tests.Controllers
{
    public class UserControllerTests 
    {
        private readonly InMemoryApplicationFactory _factory;
        private readonly HttpClient _client;

        const string BaseUrl = "api/users";

        public UserControllerTests()
        {
            _factory = new InMemoryApplicationFactory();
            _client = _factory.CreateClient();
        }

        [Theory]
        [InlineData("api/users")]
        public async Task get_should_return_success(string url)
        {
           
            var response = await _client.GetAsync(url);
            
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            var responseEntity = JsonConvert.DeserializeObject<User[]>(responseContent);

            responseEntity.ShouldNotBeNull();
            responseEntity.Count().ShouldBe(2);

        }

        [Fact]
        public async Task get_by_id_should_return_item_data()
        {
            const string id = "ad93ac22-14db-4c1b-9133-85ddb60f026d";

            var response = await _client.GetAsync($"api/users/{id}");
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            var responseEntity = JsonConvert.DeserializeObject<User>(responseContent);

            responseEntity.ShouldNotBeNull();
            responseEntity.Name.ShouldBe("TestUser2");
        }


        [Fact]
        public async Task get_by_nonexistent_id_should_return_404()
        {
            const string id = "6e977a84-7305-4842-81c8-08daf47f009a";

            var response = await _client.GetAsync($"api/users/{id}");

            response.StatusCode.ShouldBe(HttpStatusCode.NotFound);

        }


        [Fact]
        public async Task addUser_should_create_new_user()
        {
            AddUserRequest addUserRequest = new()
            {
                Name = "TestUser3",
            };

            var result = await _client.PostAsJsonAsync("api/users", addUserRequest);

            result.EnsureSuccessStatusCode();
            result.Headers.Location.ShouldNotBeNull();
            Console.WriteLine(result.Headers.Location.ToString());
 
        }

        [Fact]
        public async Task addUser_shouldNot_create_new_user()
        {
            AddUserRequest addUserRequest = new()
            {
                Name = null,
            };

            var result = await _client.PostAsJsonAsync("api/users", addUserRequest);

            result.IsSuccessStatusCode.ShouldBeFalse();

        }

        [Fact]
        public async Task editUser_should_edit()
        {
            EditUserRequest editUserRequest = new()
            {
                Id = new Guid("ad93ac22-14db-4c1b-9133-85ddb60f026d"),
                Name = "EditUserTest3"
            };

            var result = await _client.PutAsJsonAsync("api/users/ad93ac22-14db-4c1b-9133-85ddb60f026d", editUserRequest);

            result.EnsureSuccessStatusCode();

        }

        //Send put request with nonexistend UserId
        [Fact]
        public async Task editUser_shouldNot_edit()
        {
            EditUserRequest editUserRequest = new()
            {
                Id = new Guid("ad93ac22-14db-4c1b-9133-85ddb60f026d"),
                Name = ""
            };

            
            var result = await _client
                .PutAsJsonAsync("api/users/768f4a60-56aa-4cb4-9ae2-871ff71d75f6"
                , editUserRequest);

            result.StatusCode.ShouldBe(HttpStatusCode.BadRequest);

        }

        [Fact]
        public async Task editUser_shouldNot_edit_nameIsEmpty()
        {
            EditUserRequest editUserRequest = new()
            {
                Id = new Guid("ad93ac22-14db-4c1b-9133-85ddb60f026d"),
                Name = ""
            };

            var result = await _client
                .PutAsJsonAsync("api/users/ad93ac22-14db-4c1b-9133-85ddb60f026d"
                , editUserRequest);

            result.StatusCode.ShouldBe(HttpStatusCode.BadRequest);

        }

        [Fact]
        public async Task editUser_shouldNot_edit_nameLengthSmallerThanThree()
        {
            EditUserRequest editUserRequest = new()
            {
                Id = new Guid("ad93ac22-14db-4c1b-9133-85ddb60f026d"),
                Name = "te"
            };

            var result = await _client
                .PutAsJsonAsync("api/users/ad93ac22-14db-4c1b-9133-85ddb60f026d"
                , editUserRequest);

            result.StatusCode.ShouldBe(HttpStatusCode.BadRequest);

        }

        [Fact]
        public async Task editUser_shouldNot_edit_nameLengthBiggerThanTwenty()
        {
            EditUserRequest editUserRequest = new()
            {
                Id = new Guid("ad93ac22-14db-4c1b-9133-85ddb60f026d"),
                Name = "teeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee"
            };

            var result = await _client
                .PutAsJsonAsync("api/users/ad93ac22-14db-4c1b-9133-85ddb60f026d"
                , editUserRequest);

            result.StatusCode.ShouldBe(HttpStatusCode.BadRequest);

        }

        [Fact]
        public async Task get_user_comments_by_id()
        {
            GetUserRequest getUserRequest = new()
            {
                Id = new Guid("ad93ac22-14db-4c1b-9133-85ddb60f026d")
            };

            var response = await _client.GetAsync(
                $"{BaseUrl}/{getUserRequest.Id}/comments");
               

            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            var responseEntity = JsonConvert.DeserializeObject<Comment[]>(responseContent);

            responseEntity.Count().ShouldBe(1);

        }

        [Fact]
        public async Task get_user_posts_by_id()
        {
            GetUserRequest getUserRequest = new()
            {
                Id = new Guid("f3f9f5a2-1b8e-4c12-a35b-6e4c511bd737")
            };

            var response = await _client.GetAsync(
                $"{BaseUrl}/{getUserRequest.Id}/posts");


            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            var responseEntity = JsonConvert.DeserializeObject<Post[]>(responseContent);

            responseEntity.Count().ShouldBe(1);

        }

    }
}
