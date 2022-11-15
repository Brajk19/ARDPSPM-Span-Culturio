using FluentValidation;

namespace Span.Culturio.Api.Models
{
    public class LoginDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class LoginUserValidator : AbstractValidator<LoginDto>
    {
        public LoginUserValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty()
                .MaximumLength(100);
            RuleFor(x => x.Password)
                .NotEmpty()
                .MaximumLength(255);
        }
    }
}
