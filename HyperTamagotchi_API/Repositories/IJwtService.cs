using Microsoft.AspNetCore.Identity;

namespace HyperTamagotchi_API.Repositories;

public interface IJwtService
{
    string CreateJWTToken(IdentityUser user, List<string> roles);
}