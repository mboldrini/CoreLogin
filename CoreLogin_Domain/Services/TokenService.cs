using CoreLogin_Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CoreLogin_Application.Services
{
  public static class TokenService
  {
    public static string GenerateToken(User user)
    {
      string jsonSecret = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("JsonSecret").Value;

      var tokenHandler = new JwtSecurityTokenHandler();
      var key = Encoding.ASCII.GetBytes(jsonSecret);
      var tokenDescriptor = new SecurityTokenDescriptor
      {
        Subject = new ClaimsIdentity(new Claim[]
        {
          new Claim(ClaimTypes.Name, user.UserName.ToString()),
          new Claim(ClaimTypes.Email, user.Email.ToString()),
          new Claim(ClaimTypes.NameIdentifier, user.Uid.ToString()),
        }),
        Expires = DateTime.UtcNow.AddHours(2),
        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
      };
      var token = tokenHandler.CreateToken(tokenDescriptor);
      return tokenHandler.WriteToken(token);
    }
  }
}
