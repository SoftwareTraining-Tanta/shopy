
using Microsoft.AspNetCore.Mvc;
namespace Northwind.WebApi.Controllers;

using Shopy.Models;
using Shopy.Models.Dtos;
using Shopy.Models.Shared;

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
    [HttpGet("id={id:int}")]
    public ClientDto Get(int id)
    {
        ClientDto client = Client.Get(id).AsDto();
        return client;
    }
    [HttpGet("limit={limit:int}")]
    public dynamic GetLimit(int limit)
    {
        return Client.AllClients(limit).AsDto();
    }
    [HttpPut("id={id}/value={value}/Properity={properity}")]
    public string update(int id, string value)
    {
        return Client.Update(id, value);
    }
    [HttpPost]
    public string Add(ClientDto client)
    {
        return Client.Add(client.AsNormal());
    }
    [HttpDelete]
    public dynamic Delete(int id)
    {
        return Client.Delete(id);
    }
}
