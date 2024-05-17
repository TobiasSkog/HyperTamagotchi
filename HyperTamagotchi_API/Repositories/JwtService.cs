using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HyperTamagotchi_API.Repositories;

public class JwtService(IConfiguration configuration) : IJwtService
{
    private readonly IConfiguration _configuration = configuration;

    public string CreateJWTToken(IdentityUser user, List<string> roles)
    {

        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, user.UserName!),
            new(ClaimTypes.Email, user.Email!),
        };

        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            _configuration["Jwt:Issuer"],
            _configuration["Jwt:Audience"],
            claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
//public bool ValidateToken(string token)
//{
//    var tokenValidationParameters = new TokenValidationParameters
//    {
//        ValidateIssuerSigningKey = true,
//        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!)),
//        ValidateIssuer = true,
//        ValidIssuer = _configuration["Jwt:Issuer"],
//        ValidateAudience = true,
//        ValidAudience = _configuration["Jwt:Audience"],
//        ValidateLifetime = true
//    };

//    var tokenHandler = new JwtSecurityTokenHandler();

//    var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken validatedToken);

//    var jwtToken = (JwtSecurityToken)validatedToken;

//    return true;
//}
