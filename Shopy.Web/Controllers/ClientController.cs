
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
    [HttpGet("username={username}")]
    public ClientDto Get(string username)
    {
        ClientDto client = Client.Get(username).AsDto();
        return client;
    }
    [HttpGet("limit={limit:int}")]
    public dynamic GetLimit(int limit)
    {
        return Client.AllClients(limit).AsDto();
    }
    [HttpPut("username={username}/value={value}/Properity={properity}")]
    public string update(string username, string value)
    {
        return Client.Update(username, value);
    }
    [HttpPost]
    public string Add(ClientDto client)
    {
        return Client.Add(client.AsNormal());
    }
    [HttpDelete]
    public dynamic Delete(string username)
    {
        return Client.Delete(username);
    }
}
