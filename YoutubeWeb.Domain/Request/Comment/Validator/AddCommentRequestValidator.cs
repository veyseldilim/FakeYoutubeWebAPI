using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoutubeWeb.Domain.Request.Comment.Validator
{
    public class AddCommentRequestValidator : AbstractValidator<AddCommentRequest>
    {
        public AddCommentRequestValidator() 
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId cannot be empty");
            RuleFor(x => x.PostId).NotEmpty().WithMessage("PostId cannot be empty");
            RuleFor(x => x.Body).NotEmpty().WithMessage("Body cannot be empty")
                .Length(3, 140).WithMessage("Body length must be between 3 and 140");



        }
    }
}
