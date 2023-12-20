using System.IdentityModel.Tokens.Jwt;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace backend.Services;

public static class JwtService {
    public static string Encode(Claim[] claims) {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("SECRET")));

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddDays(30), // Token expiration time
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
        );

        // Serialize the token to a string
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public static User GetUserFromJwt(string jwt, Context context, Expression<Func<User, dynamic>> include = null) {
        var claim = new JwtSecurityTokenHandler().ReadJwtToken(jwt).Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
        if(claim is null)
            throw new Exception("NameIdentifyer missing in token");
        
        if (include == null) 
            return context.users.FirstOrDefault(x => x.ID.ToString() == claim.Value);
        else 
            return context.users.Include(include).FirstOrDefault(x => x.ID.ToString() == claim.Value);
    }
}