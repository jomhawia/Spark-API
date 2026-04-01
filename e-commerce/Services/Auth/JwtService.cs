using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using e_commerce.Entites;
using Microsoft.IdentityModel.Tokens;

namespace e_commerce.Services.Auth;

public interface IJwtService
{
    string CreateToken(User user);
}

public class JwtService : IJwtService
{
    private readonly IConfiguration _config;

    public JwtService(IConfiguration config)
    {
        _config = config;
    }

    public string CreateToken(User user)
    {
        var key = _config["Jwt:Key"]!;
        var issuer = _config["Jwt:Issuer"]!;
        var audience = _config["Jwt:Audience"]!;
        var expiresMinutes = int.Parse(_config["Jwt:ExpiresMinutes"]!);

        Console.WriteLine(key);
        Console.WriteLine(issuer);
        Console.WriteLine(audience);
        Console.WriteLine(expiresMinutes);

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email ?? ""),
            new Claim("firstName", user.FirstName ?? ""),
            new Claim("lastName", user.LastName ?? ""),
            new Claim(ClaimTypes.Role, user.Role) ,
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        foreach (Claim c in claims)
        {
            Console.WriteLine(c.ToString());
        }


        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var creds = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

        Console.WriteLine(signingKey);
        Console.WriteLine(creds);


        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(expiresMinutes),
            signingCredentials: creds
        );

        Console.WriteLine(token);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
