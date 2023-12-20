using System.ComponentModel.DataAnnotations;

namespace backend.Models;

public class Car {
    [Key]
    public int ID { get; set; }
    public int BrandID { get; set; }
    public int AuctionID { get; set; }
    public int UserID { get; set; }
    public string Name { get; set; }
    public int KilometersDriven { get; set; }
    public decimal KilometersPerLiter { get; set; }
    public int ModelYear { get; set; }
    public string LatestPlate { get; set; }

    public Brand Brand { get; set; }
    public User User { get; set; }
    public ICollection<Auction> Auctions { get; set; }
}