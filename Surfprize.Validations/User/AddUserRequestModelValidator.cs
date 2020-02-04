using FluentValidation;
using Surfprize.Models.User;

namespace Surfprize.Validations.User
{
    public class AddUserRequestModelValidator : AbstractValidator<AddUserRequestModel>
    {
        public AddUserRequestModelValidator()
        {
            RuleFor(u => u.Email)
                .EmailAddress()
                .NotEmpty()
                .NotNull();
            RuleFor(u => u.Role)
                .NotEmpty()
                .NotNull();
        }
    }
}
