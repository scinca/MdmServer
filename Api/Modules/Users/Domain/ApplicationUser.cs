// ApplicationUser.cs

using Microsoft.AspNetCore.Identity;

namespace Api.Modules.Users.Domain;

public sealed class ApplicationUser : IdentityUser<Guid>
{

}
