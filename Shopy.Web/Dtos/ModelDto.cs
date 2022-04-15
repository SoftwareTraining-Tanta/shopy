#nullable disable
public class ModelDto
{
    public string Name { get; set; }
    public string ImagePath { get; set; }
    public decimal Price { get; set; }
    public decimal? SalePrice { get; set; }
    public decimal? Rate { get; set; }
    public string Brand { get; set; }
    public string Color { get; set; }
    public string Category { get; set; }
    public string VendorUsername { get; set; }
    public string Features { get; set; }
    public bool IsSale { get; set; }
}