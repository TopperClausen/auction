using backend.Models;
using backend.Models.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers;

public class UsersController : Controller {

    [HttpGet]
    [Route("/users")]
    public IEnumerable<User> Index() {
        Context ctx = new Context();

        return ctx.users.AsEnumerable();
    }

    [HttpPost]
    [Route("/users")]
    public Dictionary<string, dynamic> Create([FromBody] EmailPasswordRequest req) {
        Context ctx = new Context();

        User user = new User {
            Email = req.Email,
            PasswordDigest = req.Password
        };

        ctx.users.Add(user);
        var dict = new Dictionary<string, dynamic>();
        try {
            ctx.SaveChanges();
            dict.Add("jwt", "this is the jwt");
        } catch (DbUpdateException) {
            dict.Add("error", "Email already in use");
        }

        return dict;
    }
}