using AutoMapper;
using backend.Models;
using backend.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers;

public class CarsController : BaseController {
    public CarsController(Context context, IMapper mapper) : base(context, mapper) {
    }

    [HttpGet("users/{id}/cars")]
    public IActionResult All([FromRoute] int id) {
        var user = currentUser(u => u.Cars);
        if(user.ID != id) 
            return new UnauthorizedObjectResult(new { Message = "You do not have access to this content" });
        
        var map = Mapper.Map<List<CarDTO>>(user.Cars);
        return new OkObjectResult(map);
    }

    [HttpPost("users/{id}/cars")]
    public IActionResult Create([FromRoute] int id, [FromBody] Car car) {
        var user = currentUser();
        if(user.ID != id) 
            return new UnauthorizedObjectResult(new { Message = "You do not have access to this content" });
        
        try {
            Context.Cars.Add(car);
            Context.SaveChanges();
            return new OkResult();
        } catch(DbUpdateException e) {
            return new UnprocessableEntityObjectResult(ErrorBody(e));
        }
    }
}