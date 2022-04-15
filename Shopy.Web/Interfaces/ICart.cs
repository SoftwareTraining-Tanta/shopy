using Shopy.Web.Models;
namespace Shopy.Web.Interfaces;
public interface ICart
{
    void UpdateCity(string clientUsername, string value);
    void UpdatePhone(string clientUsername, string value);
    bool Exist(string clientUsername);
    Cart Get(string clientUsername);
    int Count(string clientUsername);
    void AddToCart(string ClientUsername, string modelName);
    decimal TotalPrice(string clientUsername);
    string RemoveFromCart(int ProductId);
    string CheckOut(string clientUsername);
    List<Product> InCart(string clientUsername);
}