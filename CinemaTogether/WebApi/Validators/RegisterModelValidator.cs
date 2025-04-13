using FluentValidation;
using WebApi.Models;

namespace WebApi.Validators;

public class RegisterModelValidator : AbstractValidator<RegisterModel>
{
    public RegisterModelValidator()
    {
        RuleFor(c => c.Email)
            .NotEmpty()
            .EmailAddress()
            .WithMessage(c => $"{c.Email} is not a valid email");

        RuleFor(c => c.Password)
            .NotEmpty()
            .MinimumLength(6).WithMessage("Password must be at least 6 characters long");
    }
}