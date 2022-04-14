using System;
namespace Shopy.Web.Shared;

public class MyExceptions
{
    public static string ClientAlreadyExist(string username) => $"Username {username} or email or phone number is already in use";
    public static string ClientNotFound(string username) => $"Client with username : {username} is not found";
    public static string VendortNotFound(string username) => $"Vendor with username : {username} is not found";
    public static string CartNotFound(string username) => $"Cart with username : {username} is not found";
    public static string ProductNotFound(int id) => $"Product with id : {id} is not found";
    public static string PhoneNumberIsUsed(string phone) => $"Phone number : {phone} is used before";
    public static string EmailIsUsed(string email) => $"Email : {email} is already used";

}