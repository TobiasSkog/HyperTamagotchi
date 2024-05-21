using HyperTamagotchi_API.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace HyperTamagotchi_API.Repositories;

public interface IJwtService
{
    string CreateJWTToken(IdentityUser user, List<string> roles, bool rememberMe);
    ClaimsPrincipal ValidateToken(string token);
    Task<bool> ValidateRefreshToken(Customer customer, string refreshToken);
    string GenerateRefreshToken();
    ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    string GenerateJwtToken(IEnumerable<Claim> claims);
}