using backend.Models;
using backend.Models.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers;

public class UsersController : Controller {

    Context Context;
    public UsersController(Context ctx) {
        this.Context = ctx;
    }

    [HttpGet]
    [Route("/users")]
    public IEnumerable<User> Index() {
        return Context.users.AsEnumerable();
    }

    [HttpPost]
    [Route("/users")]
    public IActionResult Create([FromBody] EmailPasswordRequest req) {
        User user = new User {
            Email = req.Email,
            PasswordDigest = req.Password
        };

        Context.users.Add(user);
        try {
            Context.SaveChanges();
            return new OkObjectResult(new { jwt = user.Jwt() });
        } catch (DbUpdateException) {
            return new ConflictObjectResult(new { error = "Email already in use" });
        }
    }
}