using FluentValidation.TestHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeWeb.Domain.Request.Comment;
using YoutubeWeb.Domain.Request.Comment.Validator;

namespace YoutubeWeb.Domain.Tests.Requests.Comment.Validators
{
    public class EditCommentRequestValidatorTests
    {
        private readonly EditCommentRequestValidator _editCommentRequestValidator;

        public EditCommentRequestValidatorTests()
        {
            _editCommentRequestValidator = new EditCommentRequestValidator();
        }


        //  public Guid Id { get; set; }

        //  public string Body { get; set; }

        [Fact]
        public void should_have_error_when_body_is_null()
        {
            var editCommentRequest = new EditCommentRequest()
            {
                Id = Guid.NewGuid(),
                Body = null,
                
            };

            _editCommentRequestValidator.TestValidate(editCommentRequest)
                .ShouldHaveValidationErrorFor(x => x.Body);
        }

        [Fact]
        public void should_have_error_when_body_is_empty()
        {
            var editCommentRequest = new EditCommentRequest()
            {
                Id = Guid.NewGuid(),
                Body = "",

            };

            _editCommentRequestValidator.TestValidate(editCommentRequest)
                .ShouldHaveValidationErrorFor(x => x.Body);
        }

        [Fact]
        public void should_have_error_when_bodyLength_is_smaller_than_3()
        {
            var editCommentRequest = new EditCommentRequest()
            {
                Id = Guid.NewGuid(),
                Body = "aa"
                
            };

            _editCommentRequestValidator.TestValidate(editCommentRequest)
                .ShouldHaveValidationErrorFor(x => x.Body);
        }


        [Fact]
        public void should_not_have_error_when_bodyLength_is_bigger_than_3()
        {
            var editCommentRequest = new EditCommentRequest()
            {
                Id = Guid.NewGuid(),
                Body = "aaaa"
            };

            _editCommentRequestValidator.TestValidate(editCommentRequest)
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
            var editCommentRequest = new EditCommentRequest()
            {
                Id = Guid.NewGuid(),
                Body = s
            };

            _editCommentRequestValidator.TestValidate(editCommentRequest)
                .ShouldHaveValidationErrorFor(x => x.Body);
        }
    }
}
