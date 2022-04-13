namespace Shopy.Web.Interfaces;
using Shopy.Web.Models;
public interface IVendor
{
    void Add(Vendor vendor);
    Vendor Get(string username);
    bool Exist(string username);
    void Delete(string username);
    void UpdatePassword(string username, string newPassword);
    void UpdateVerificationCode(string username, string verificationCode);
    List<Vendor> AllVendors(int limit);
    List<Model> VendorModels(string Username);
}