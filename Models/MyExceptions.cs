using System;
namespace Shopy.Models;

public class MyExceptions
{
    public static string ClientNotFound(int id) => $"Client with id : {id} is not found";
    public static string VendortNotFound(int id) => $"Vendor with id : {id} is not found";
    public static string CartNotFound(int id) => $"Cart with id : {id} is not found";
    public static string InvalidDeletion(int id) => $"Can't Delete This Client";
    public static string ProductNotFound(int id) => $"Product with id : {id} is not found";
    public static string PhoneNumberIsUsed(string phone) => $"Phone number : {phone} is used before";
    public static string EmailIsUsed(string email) => $"Email : {email} is already used";

}
