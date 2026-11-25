// OnboardingEndpoint.cs

using Api.SharedKernel.Persistence;
using FastEndpoints;
using FluentValidation;
using Wolverine;

namespace Api.Modules.Tenants.Features.Onboarding;

public static class OnboardingEndpoint
{
    public record Request(string CompanyName, string Email, string Password);

    public class RequestValidator : Validator<Request>
    {
        public RequestValidator()
        {
            RuleFor(x => x.CompanyName)
                .NotEmpty()
                .WithMessage("Company name is required")
                .MaximumLength(256)
                .WithMessage("Company name cannot exceed 256 characters");

            RuleFor(x => x.Email)
                .EmailAddress()
                .WithMessage("Email is required");

            RuleFor(x => x.Password)
                .MinimumLength(4)
                .WithMessage("Password must be at least 4 characters long")
                .MaximumLength(128)
                .WithMessage("Password cannot exceed 128 characters");

        }
    }

    public class Endpoint(IMessageBus bus, AppDbContext context) : Endpoint<Request>
    {
        public override void Configure()
        {
         Post("onboarding");
         AllowAnonymous();
        }

        public override async Task HandleAsync(Request req, CancellationToken ct)
        {



        }
    }
}
