using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using AutoRainAPI.Models;
using Microsoft.IdentityModel.Tokens;

namespace AutoRainAPI.Utils;

public class AuthenticationUtils
{
    public static void CreatePasswordHash(string password, out byte[] hash, out byte[] salt)
    {
        using (var hmac = new HMACSHA512())
        {
            salt = hmac.Key;
            hash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }
    }

    public static bool VerifyPasswordHash(User user, string password)
    {
        using (var hmac = new HMACSHA512(user.Salt))
        {
            var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual(user.Password);
        }
    }

    public static string CreateToken(User user)
    {
        var claims = new List<Claim> {
            new(ClaimTypes.Role, "User"),
            new("UserId", user.UserId.ToString()),
            new(ClaimTypes.Name, $"{user.Name} {user.LastName}"),
            new(ClaimTypes.Email, user.Email),
        };


        string secretKey = GetSecretKey();
        var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(secretKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddHours(3),
            signingCredentials: credentials
        );
        
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public static string GetSecretKey() =>
         Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") is "Production"
            ? Environment.GetEnvironmentVariable("SECRET_TOKEN")!
            : string.Concat(Enumerable.Repeat("SuperMegaSecretKeyForDevEnvironment", 8));
}