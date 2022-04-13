namespace Shopy.Web.Interfaces;
using Shopy.Web.Models;
public interface IProduct
{
    Product Get(int productId);
    bool Exist(int id);
    // string Add(IProduct product);
    void UpdateModel(int ProductId, string Model);
    List<Product> AvailableProducts(int limit);
    void UpdateRate(int productId, decimal rate);
    void AddQuantity(Product product, int quanitity);
}
