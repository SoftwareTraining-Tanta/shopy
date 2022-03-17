
using Microsoft.AspNetCore.Mvc;
namespace Northwind.WebApi.Controllers;

using System;
using Shopy.Models;
using Shopy.Dtos;
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
    [HttpGet("{id:int}")]
    public ClientDto Get(int id)
    {
        return new ClientDto
        {
            Id = Client.Get(id).Id,
            Name = Client.Get(id).Name,
            City = Client.Get(id).City,
            Phone = Client.Get(id).Phone,
            Email = Client.Get(id).Email,
            Country = Client.Get(id).Country,
            Products = Client.ClientProducts(id)
        };
    }
    [HttpGet("/limit={limit:int}")]
    public dynamic GetLimit(int limit)
    {
        return Client.AllClients(limit);
    }
    public class WeatherForecast
    {
        public DateTime Date { get; set; }
        public int TemperatureC { get; set; }
        public string? Summary { get; set; }
    }
}