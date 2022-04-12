using Shopy.Web.Models;
namespace Shopy.Web.Interfaces;
public interface ICart
{
    string Update(string clientUsername, dynamic value);

    bool Exist(string clientUsername);
    dynamic Get(string clientUsername);
    int Count(string clientUsername);
    void AddToCart(string ClientUsername, int ProductId);

    decimal TotalPrice(string clientUsername);

    string RemoveFromCart(int ProductId);
    string CheckOut(string clientUsername);

    List<Product> InCart(string clientUsername);
}