using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoutubeWeb.Domain.Request.Post.Validator
{
    public class AddPostRequestValidator : AbstractValidator<AddPostRequest>
    {
        public AddPostRequestValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("UserId cannot be empty");
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title cannot be empty")
                .Length(3, 50).WithMessage("Title length must be between 3 and 50");
            RuleFor(x => x.Body).Length(0, 100).WithMessage("Body length cannot be greater than 100");
            

        }
    }
}
