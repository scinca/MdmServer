// CreateUserCommandHandler.cs

using Api.Modules.Users.Domain;
using Microsoft.AspNetCore.Identity;
using Wolverine;

namespace Api.Modules.Users.Features.CreateUser;

public class CreateUserCommandHandler(ILogger<CreateUserCommandHandler> logger,
    UserManager<ApplicationUser> userManager) : IWolverineHandler
{
    public async Task HandleAsync(
        CreateUserCommand command,
        CancellationToken cancellationToken = default)
    {

        var user = new ApplicationUser
        {
            UserName = command.Email,
            Email = command.Email,
            EmailConfirmed = false,

            TenantId = command.TenantId,
        };

        var createdResult = await userManager.CreateAsync(user, command.Password);
        if (!createdResult.Succeeded)
        {
            logger.LogError("User Creation Failed {Errors}", createdResult.Errors);
        }

        // return result
        await userManager.AddToRoleAsync(user, "Admin");
    }
}
