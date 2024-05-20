using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace HyperTamagotchi_API.Repositories;

public interface IJwtService
{
    string CreateJWTToken(IdentityUser user, List<string> roles, bool rememberMe);
    ClaimsPrincipal ValidateToken(string token);
}