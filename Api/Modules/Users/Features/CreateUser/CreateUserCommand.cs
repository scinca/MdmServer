// CreateUserCommand.cs

namespace Api.Modules.Users.Features.CreateUser;

public sealed record CreateUserCommand(string Email, string Password, Guid TenantId);
