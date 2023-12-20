using System.ComponentModel.DataAnnotations;

namespace backend.Models;

public class Bid {
    [Key]
    public int ID { get; set; }
    public int UserID { get; set; }
    public int AuctionID { get; set; }
    public decimal OfferedPrice { get; set; }

    public User User { get; set; }
    public Auction Auction { get; set; }
}