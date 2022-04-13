namespace Shopy.Web.Interfaces;
using Shopy.Web.Models;
public interface IProduct
{
    Product Get(int productId);
    List<Product> AvailableProducts(int limit);
    public Vendor GetVendor(int id);
    public string Add(Product product);
    string AddQuantity(Product product, int quanitity);
    string UpdateRate(int productId, decimal rate);
    string UpdateModel(int ProductId, string Model);
    public string UpdateClientUsername(int id, string value);
    public string UpdateCartId(int id, int? value);
    bool Exist(int id);
    // string Add(IProduct product);

}
