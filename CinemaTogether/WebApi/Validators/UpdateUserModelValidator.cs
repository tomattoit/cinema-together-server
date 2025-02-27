using FluentValidation;
using WebApi.Models;

namespace WebApi.Validators;

public class UpdateUserModelValidator : AbstractValidator<UpdateUserModel>
{
    public UpdateUserModelValidator()
    {
        RuleFor(c => c.DateOfBirth)
            .Must(dob => dob <= DateTime.Now).WithMessage("Date of birth must be in the past");
        
        RuleFor(c => c.Email).NotEmpty().WithMessage("Email is required");
        
        RuleFor(c => c.Username).NotEmpty().WithMessage("Username is required");
    }
}