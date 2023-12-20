using backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers;

public class BrandsController {

    Context Context;

    public BrandsController(Context context) {
        Context = context;
    }

    [HttpGet("/brands")]
    public IActionResult All() {
        var brands = Context.Brands.AsEnumerable();
        return new OkObjectResult(brands);
    }

    [HttpPost("/brands")]
    public IActionResult Create([FromBody] Brand brand) {
        Context.Brands.Add(brand);
        try {
            Context.SaveChanges();
            return new OkObjectResult(null);
        } catch (DbUpdateException) {
            return new ConflictObjectResult(new { Error = "Brand already exists" });
        }
    }

    [HttpGet("/brands/{id}")]
    public IActionResult Show([FromRoute] int id) {
        var brand = Context.Brands.FirstOrDefault(x => x.ID == id);
        if(brand is null)
            return new NotFoundObjectResult(new { Error = $"Brand with id {id} was not found"});

        return new OkObjectResult(brand);
    }
}