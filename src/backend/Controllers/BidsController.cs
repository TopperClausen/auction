using AutoMapper;
using backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers;

public class BidsController : BaseController
{
    public BidsController(Context context, IMapper mapper) : base(context, mapper)
    {
    }

    [HttpGet("/users/{id}/bids")]
     public IActionResult AllUserBids([FromRoute] int id) {
        var user = currentUser(x => x.Bids);
        if(user.ID != id)
            return Unauthorized();

        return new OkObjectResult(user.Bids);
    }

    [HttpGet("auctions/{id}/bids")]
    public IActionResult AllAuctionBids([FromRoute] int id) {
        var user = currentUser();
        var auction = Context.Auctions
                                    .Include(x => x.Car)
                                    .Include(x => x.Bids)
                                    .FirstOrDefault(x => x.ID == id);
        if(auction is null || (!Auction.Active(auction) && auction.Car.UserID != user.ID))
            return NotFound();
        
        return new OkObjectResult(auction.Bids);
    }

    [HttpPost("/auctions/{id}/bids")]
    public IActionResult Create([FromBody] Bid bid, [FromRoute] int id) {
        var auction = Context.Auctions.FirstOrDefault(x => x.ID == id);
        if(auction is null || !Auction.Active(auction))
            return NotFound();

        bid.AuctionID = id;
        bid.UserID = currentUser().ID;
        Context.Bids.Add(bid);
        try {
            Context.SaveChanges();
            return Ok();
        } catch (Exception e) {
            return new UnprocessableEntityObjectResult(ErrorBody(e));
        }
    }
}