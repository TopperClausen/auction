using System.Text.Json.Serialization;

namespace backend.Models.DTO;

public class CarDTO {
    public int ID { get; set; }
    public int BrandID { get; set; }
    public int AuctionID { get; set; }
    public int UserID { get; set; }
    public string Name { get; set; }
    public int KilometersDriven { get; set; }
    public decimal KilometersPerLiter { get; set; }
    public int ModelYear { get; set; }

    [JsonIgnore]
    public User User { get; set; }
}