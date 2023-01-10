using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoutubeWeb.Domain.Request.Comment.Validator
{
    public class EditCommentRequestValidator : AbstractValidator<EditCommentRequest>
    {

        public EditCommentRequestValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Edit comment Id cannot be null");
            RuleFor(x => x.Body)
                .NotEmpty().WithMessage("Comment body cannot be empty")
                .Length(3, 140).WithMessage("Body length must be between 3 and 140");
        }

    }
}
