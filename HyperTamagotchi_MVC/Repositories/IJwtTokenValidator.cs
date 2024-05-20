using System.Security.Claims;

namespace HyperTamagotchi_MVC.Repositories;

public interface IJwtTokenValidator
{
    ClaimsPrincipal ValidateToken(string token);
}
