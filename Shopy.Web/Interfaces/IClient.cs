namespace Shopy.Web.Interfaces;
using Shopy.Web.Models;
public interface IClient
{
    string Add(Client client);
    Client Get(string username);
    bool Exist(string username);
    string Delete(string username);
    void UpdatePassword(string username, string new_password);
    void UpdateVerificationCode(Client client, string verificationCode);
    List<Client> AllClients(int limit);
    List<Product> ClientProducts(string Username); // previously bought products

}