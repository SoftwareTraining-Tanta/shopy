namespace Shopy.Web.Interfaces;
using Shopy.Web.Models;
public interface IVendor
{
    string Add(Vendor vendor);
    Vendor Get(string username);
    bool Exist(string username);
    string Delete(string username);
    string UpdatePassword(string username, string newPassword);
    string UpdateVerificationCode(string username, string verificationCode);
    List<Vendor> AllVendors(int limit);
    List<Model> VendorModels(string Username);
}