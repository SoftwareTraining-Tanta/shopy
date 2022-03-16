using System;
namespace Models;

public class MyExceptions
{
    public static string ClientNotFound(int id) => $"Client with id : {id} is not found";

    public static string PhoneNumberIsUsed(string phone) => $"Phone number : {phone} is used before";

    public static string EmailIsUsed(string email) => $"Email : {email} is already used";

}
