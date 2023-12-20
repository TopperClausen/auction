using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using backend.Services;
using DevOne.Security.Cryptography.BCrypt;
using Microsoft.EntityFrameworkCore;

namespace backend.Models;

[Index(nameof(Email), IsUnique = true)]
public class User {
    private string _passwordDigest;
    
    [Key]
    public int ID { get; private set; }

    [EmailAddress]
    public string Email { get; set; }

    public ICollection<Car> Cars { get; set; }

    public string PasswordDigest {
        get { return _passwordDigest; }
        set { _passwordDigest = Encrypt(value); }
    }

    private string Encrypt(string plain) {
        string salt = BCryptHelper.GenerateSalt();
        string hash = BCryptHelper.HashPassword(plain, salt);
        return hash;
    }

    public bool Authenticate(string plain) {
        return BCryptHelper.CheckPassword(plain, PasswordDigest);
    }

    public string Jwt() {
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, ID.ToString(), "id"),
            new Claim(ClaimTypes.Email, Email, "email")
        };

        return JwtService.Encode(claims);
    }
    
}