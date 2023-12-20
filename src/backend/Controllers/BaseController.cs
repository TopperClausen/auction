using System.Linq.Expressions;
using AutoMapper;
using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;
public class BaseController : Controller {
    public Context Context;
    public IMapper Mapper;

    public BaseController(Context context, IMapper mapper) {
        Context = context;
        Mapper = mapper;
    }

    public string Jwt() {
        return HttpContext.Request.Headers["Authorization"].ToString().Split(" ")[1];
    }

    public User currentUser(Expression<Func<User, dynamic>> include = null) {
        return JwtService.GetUserFromJwt(Jwt(), Context, include);
    }

    public Object ErrorBody(Exception e) {
        if(Environment.GetEnvironmentVariable("ENVIROMENT") == "DEVELOPMENT") {
            return new {
                Message = "An error has occured",
                Error = e.Message,
                Inner = e.InnerException.ToString()
            };
        } else {
            return new {
                Message = "An error has occured"
            };
        }
    }
}