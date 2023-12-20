using AutoMapper;
using backend.Models;
using backend.Models.Requests;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

public class SessionsController : BaseController {
    public SessionsController(Context context, IMapper mapper) : base(context, mapper)
    {
    }

    [HttpPost]
    [Route("/sessions")]
    public IActionResult Create([FromBody] EmailPasswordRequest req) {
        var user = Context.users.FirstOrDefault(u => u.Email == req.Email);
        if(user is null || !user.Authenticate(req.Password)) {
            return new UnauthorizedObjectResult(new { Error = "Incorrect credentials" });
        }

        return new OkObjectResult(new { Jwt = user.Jwt() });
    }
}