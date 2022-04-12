namespace Shopy.Web.Interfaces;
using Shopy.Web.Models;
public interface IProduct
{
    Product Get(int productId);
    bool Exist(int id);
    // string Add(IProduct product);
    List<Product> AvailableProducts();
    void UpdateRate(int productId, float rate);
    Model GetModel(int productId);
}
