using System.Data.Common;
using AutoMapper;
using backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers;

public class AuctionsController : BaseController
{
    public AuctionsController(Context context, IMapper mapper) : base(context, mapper)
    {
    }

    [HttpGet("/auctions")]
    public IActionResult All() {
        var activeAuctions = Context.Auctions.Where(x => Auction.Active(x)).ToList();
        return new OkObjectResult(activeAuctions);
    }

    [HttpPost("cars/{id}/auctions")]
    public IActionResult Create([FromRoute] int id, [FromBody] Auction auction) {
        var car = Context.Cars.FirstOrDefault(x => x.ID == id);
        if(car is null)
            return NotFound();
        
        var user = currentUser();
        if(user.ID != car.UserID)
            return Unauthorized();

        if(auction.CarID != id)
            return new ConflictObjectResult(new { error = "ID from route and body mismatch" });
        
        Context.Auctions.Add(auction);

        try {
            Context.SaveChanges();
            return Ok();
        } catch (DbUpdateException e) {
            return new UnprocessableEntityObjectResult(new { message = "An unknown error has occured", internalError = e.Message });
        }
    }

    [HttpDelete("/auctions/{id}")]
    public IActionResult Delete([FromRoute] int id) {
        var auction = Context.Auctions.FirstOrDefault(x => x.ID == id);
        if (auction is null)
            return NotFound();

        if(auction.Car.UserID != currentUser().ID)
            return Unauthorized();

        auction.EndingAt = DateTime.Now;
        Context.SaveChanges();
        return Ok();
    }
}