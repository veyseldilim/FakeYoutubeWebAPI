using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoutubeWeb.Domain.Request.User.Validator
{
    public class EditUserRequestValidator : AbstractValidator<EditUserRequest>
    {
        public EditUserRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("User name cannot be empty")
                .Length(3, 20).WithMessage("Name length must be between 3 and 20");
        }
    }
}
