
using Microsoft.AspNetCore.Mvc;
namespace Northwind.WebApi.Controllers;

using Shopy.Web.Models;
using Shopy.Web.Dtos;
using Shopy.Web.Shared;

[ApiController]
[Route("api/clients/")]
public class ClientController : ControllerBase
{

    // private readonly ILogger<WeatherForecastController> _logger;
    // public WeatherForecastController(
    //   ILogger<WeatherForecastController> logger)
    // {
    //     _logger = logger;
    // }
    [HttpGet("get/username={username}")] // api/client/username={username}
    public ClientDto Get(string username)
    {
        Client Client = new();
        ClientDto client = Client.Get(username).AsDto();
        return client;
    }
    [HttpGet("getlimit/limit={limit:int}")]
    public dynamic GetLimit(int limit)
    {
        Client Client = new();

        return Client.AllClients(limit).AsDto();
    }
    [HttpPut("username={username}/value={value}/Properity={properity}")]
    public string update(string username, string value)
    {
        Client Client = new();

        return Client.Update(username, value);
    }
    [HttpPost]
    public string Add(ClientDto client)
    {
        Client Client = new();

        return Client.Add(client.AsNormal());
    }
    [HttpDelete]
    public dynamic Delete(string username)
    {
        Client Client = new();

        return Client.Delete(username);
    }
    [HttpPost("signin/{username}/{password}")]
    public ActionResult SignIn(string username, string password)
    {
        try
        {
            Client Client = new();

            if (!Client.Exist(username))
            {
                return NotFound(MyExceptions.ClientNotFound(username));
            }

            ClientDto client = Client.Get(username).AsDto();
            if (client.Password.ToSha256() == password)
            {
                return Ok(client.Username);
            }
            return BadRequest("Wrong password");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpPost("SignUp")]
    public ActionResult SignUp(ClientDto clientDto)
    {
        Client client = clientDto.AsNormal();
        string VerificationCode = new Random().Next(100000, 999999).ToString();
        try
        {
            Smtp.SendMessage(
            toEmail: clientDto.Email,
            subject: "Verification code for Shopy",
            body: "Please enter this code to verify your account : " + VerificationCode);
        }
        catch
        {
            return BadRequest("Error sending verification code");
        }
        Client Client = new();

        Client.UpdateVerificationCode(client, VerificationCode);
        Client.Add(client);
        return Ok(VerificationCode);

    }
    [HttpGet("Verify/{username}/{verificationCode}")]
    public ActionResult Verify(string username, string verificationCode)
    {
        Client Client = new();

        Client client = Client.Get(username);
        try
        {
            if (client.VerificationCode != verificationCode)
            {
                return BadRequest("Verification Code is wrong");
            }
            using (ShopyCtx db = new())
            {
                client.IsVerified = true;
                db.SaveChanges();
            }
            return Ok(client.Username);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    //ask michael to delete the client if not verified
}

//api/clients/Get(username={username})

