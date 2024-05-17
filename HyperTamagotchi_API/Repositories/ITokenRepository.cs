using Microsoft.AspNetCore.Identity;

namespace HyperTamagotchi_API.Repositories;

public interface ITokenRepository
{
    string CreateJWTToken(IdentityUser user, List<string> roles);
}