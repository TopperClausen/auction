using System.ComponentModel.DataAnnotations;
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
    
}