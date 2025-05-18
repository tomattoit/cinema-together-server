using FluentValidation;
using WebApi.Models;

namespace WebApi.Validators;

public class UpdatePasswordModelValidator : AbstractValidator<UpdatePasswordModel>
{
    public UpdatePasswordModelValidator()
    {
        RuleFor(c => c.NewPassword)
            .NotEmpty()
            .MinimumLength(6).WithMessage("Password must be at least 6 characters long");
    }
}