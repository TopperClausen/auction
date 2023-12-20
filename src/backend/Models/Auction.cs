using System.ComponentModel.DataAnnotations;

namespace backend.Models;

public class Auction {
    [Key]
    public int ID { get; set; }
    public int CarID { get; set; }
    public decimal StartingPrice { get; set; }
    public decimal LowestSalePrice { get; set; }
    public decimal BuyNowPrice { get; set; }
    public DateTime StartedAt { get; set; }
    public DateTime EndingAt { get; set; }

    public Car Car { get; set; }

    public static bool Active(Auction auction) {
        return auction.StartedAt > DateTime.Today && auction.EndingAt < DateTime.Today;
    }
}