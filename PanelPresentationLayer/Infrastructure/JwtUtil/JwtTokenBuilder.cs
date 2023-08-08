using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using PanelViewModel.IdentitiesViewModels;

namespace PanelPresentationLayer.Infrastructure.JwtUtil;

public class JwtTokenBuilder
{
    public static string BuildToken(UserViewModel user, IConfiguration configuration)
    {
        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
        };
        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtConfig:SignInKey"]));
        var credential = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: configuration["JwtConfig:Issuer"],
            audience: configuration["JwtConfig:Audience"],
            claims: claims,
            expires: DateTime.Now.AddDays(30),
            signingCredentials: credential);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}