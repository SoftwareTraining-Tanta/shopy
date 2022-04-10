using Microsoft.AspNetCore.Mvc;
namespace Northwind.WebApi.Controllers;

using Shopy.Web.Models;
using Shopy.Web.Dtos;
using Shopy.Web.Shared;

[ApiController]
[Route("api/carts/")]
public class CartController : ControllerBase
{
    [HttpPost("{customerUsername}/{pid:int}")]
    public string Add(string customerUsername, int pid)
    {
        return Cart.AddToCart(customerUsername, pid);
    }
    [HttpGet("clientUsername={clientUsername}")]
    public CartDto Get(string clientUsername)
    {
        Cart cart = Cart.Get(clientUsername);
        return new CartDto
        {
            City = cart.City,
            Country = cart.Country,
            Email = cart.Email,
            Phone = cart.Phone,
            Products = Cart.InCart(clientUsername).AsDto()
        };
    }
    [HttpPut("id={id}/value={value}/Properity={properity}")]
    public string update(string clientUsername, string value)
    {
        return Cart.Update(clientUsername, value);
    }
    [HttpGet("{clientUsername}")]
    public int GetCount(string clientUsername)
    {
        return Cart.Count(clientUsername);
    }
    [HttpGet("totPriceByUsername={clientUsername}")]
    public decimal PriceById(string clientUsername)
    {
        return Cart.TotalPrice(clientUsername);
    }
    [HttpDelete("productId={id:int}")]
    public string Delete(int id)
    {
        return Cart.RemoveFromCart(id);
    }


}