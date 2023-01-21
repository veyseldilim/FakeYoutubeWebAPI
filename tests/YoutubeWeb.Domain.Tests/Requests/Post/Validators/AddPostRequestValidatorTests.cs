using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeWeb.Domain.Request.Comment.Validator;
using YoutubeWeb.Domain.Request.Comment;
using YoutubeWeb.Domain.Request.Post.Validator;
using YoutubeWeb.Domain.Request.Post;
using FluentValidation.TestHelper;

namespace YoutubeWeb.Domain.Tests.Requests.Post.Validators
{
    public class AddPostRequestValidatorTests
    {
       

        private readonly AddPostRequestValidator _addPostRequestValidator;

        public AddPostRequestValidatorTests()
        {
            _addPostRequestValidator = new AddPostRequestValidator();
        }


        /*
        [Fact]
        public void should_have_error_when_userId_is_null()
        {
            var addPostRequest = new AddPostRequest()
            {
                Title = "Title",
                Body = "Body",
                UserId = null
            };

            _addPostRequestValidator.TestValidate(addPostRequest)
                .ShouldHaveValidationErrorFor(x => x.UserId);
        }

        */

        [Fact]
        public void should_have_error_when_title_is_null()
        {
            var addPostRequest = new AddPostRequest()
            {
                Title = null,
                Body = "Body",
                UserId = Guid.NewGuid()
            };

            _addPostRequestValidator.TestValidate(addPostRequest)
                .ShouldHaveValidationErrorFor(x => x.Title);
        }

        [Fact]
        public void should__have_error_when_body_is_empty()
        {
            var addPostRequest = new AddPostRequest()
            {
                Title = "",
                Body = "Body",
                UserId = Guid.NewGuid()
            };

            _addPostRequestValidator.TestValidate(addPostRequest)
                .ShouldHaveValidationErrorFor(x => x.Title);
        }

        [Fact]
        public void should_have_error_when_titleLength_smaller_than_3()
        {
            var addPostRequest = new AddPostRequest()
            {
                Title = "aa",
                Body = "Body",
                UserId = Guid.NewGuid()
            };

            _addPostRequestValidator.TestValidate(addPostRequest)
                .ShouldHaveValidationErrorFor(x => x.Title);
        }

        [Fact]
        public void should_not_have_error_when_bodyLength_is_bigger_than_3()
        {
            var addPostRequest = new AddPostRequest()
            {
                Title = "aaaas",
                Body = "Body",
                UserId = Guid.NewGuid()
            };

            _addPostRequestValidator.TestValidate(addPostRequest)
                .ShouldNotHaveAnyValidationErrors();
        }

      

        [Fact]
        public void should__have_error_when_bodyLength_is_bigger_than_3()
        {
            string s = "";

            for (int i = 0; i < 145; i++)
            {
                s += "a";
            }


            var addPostRequest = new AddPostRequest()
            {
                Title = s,
                Body = "Body",
                UserId = Guid.NewGuid()
            };

            _addPostRequestValidator.TestValidate(addPostRequest)
                .ShouldHaveValidationErrorFor(x => x.Title);
        }
    }
}
