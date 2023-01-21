using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeWeb.Domain.Request.Comment;
using YoutubeWeb.Domain.Request.Comment.Validator;
using FluentValidation.TestHelper;

namespace YoutubeWeb.Domain.Tests.Requests.Comment.Validators
{
    public class AddCommentRequestValidatorTests
    {
        private readonly AddCommentRequestValidator _addCommentRequestValidator;

        public AddCommentRequestValidatorTests()
        {
            _addCommentRequestValidator = new AddCommentRequestValidator();
        }

        /*
        [Fact]
        public void should_have_error_when_PostId_is_null()
        {
            var addCommentRequest = new AddCommentRequest() 
            {
                PostId = null,
                Body = "Body",
                UserId = Guid.NewGuid()
            };

            _addCommentRequestValidator.TestValidate(addCommentRequest)
                .ShouldHaveValidationErrorFor(x => x.PostId);
        }

        

        [Fact]
        public void should_have_error_when_userId_is_null()
        {
            var addCommentRequest = new AddCommentRequest()
            {
                PostId = Guid.NewGuid(),
                Body = "Body",
                UserId = null
            };

            _addCommentRequestValidator.TestValidate(addCommentRequest)
                .ShouldHaveValidationErrorFor(x => x.UserId);
        }

        */

        [Fact]
        public void should_have_error_when_body_is_null()
        {
            var addCommentRequest = new AddCommentRequest()
            {
                PostId = Guid.NewGuid(),
                Body = null,
                UserId = Guid.NewGuid()
            };

            _addCommentRequestValidator.TestValidate(addCommentRequest)
                .ShouldHaveValidationErrorFor(x => x.Body);
        }

        [Fact]
        public void should_have_error_when_body_is_empty()
        {
            var addCommentRequest = new AddCommentRequest()
            {
                PostId = Guid.NewGuid(),
                Body = "",
                UserId = Guid.NewGuid()
            };

            _addCommentRequestValidator.TestValidate(addCommentRequest)
                .ShouldHaveValidationErrorFor(x => x.Body);
        }

        [Fact]
        public void should_have_error_when_bodyLength_is_smaller_than_3()
        {
            var addCommentRequest = new AddCommentRequest()
            {
                PostId = Guid.NewGuid(),
                Body = "aa",
                UserId = Guid.NewGuid()
            };

            _addCommentRequestValidator.TestValidate(addCommentRequest)
                .ShouldHaveValidationErrorFor(x => x.Body);
        }


        [Fact]
        public void should_not_have_error_when_bodyLength_is_bigger_than_3()
        {
            var addCommentRequest = new AddCommentRequest()
            {
                PostId = Guid.NewGuid(),
                Body = "aaaa",
                UserId = Guid.NewGuid()
            };

            _addCommentRequestValidator.TestValidate(addCommentRequest)
                .ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void should__have_error_when_bodyLength_is_bigger_than_3()
        {
            string s = "";

            for(int i = 0; i < 145; i++)
            {
                s += "a";
            }
            var addCommentRequest = new AddCommentRequest()
            {
                PostId = Guid.NewGuid(),
                Body = s,
                UserId = Guid.NewGuid()
            };

            _addCommentRequestValidator.TestValidate(addCommentRequest)
                .ShouldHaveValidationErrorFor(x => x.Body);
        }
    }
}
