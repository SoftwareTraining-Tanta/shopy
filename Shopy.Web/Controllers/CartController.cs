using Microsoft.AspNetCore.Mvc;
namespace Northwind.WebApi.Controllers;

using Shopy.Models;
using Shopy.Models.Dtos;
using Shopy.Models.Shared;

[ApiController]
[Route("api/carts/")]
public class CartController : ControllerBase
{
    [HttpPost("{cid:int}/{pid:int}")]
    public string Add(int cid, int pid)
    {
        return Cart.AddToCart(cid, pid);
    }
    [HttpGet("clientId={clientId:int}")]
    public CartDto Get(int clientId)
    {
        Cart cart = Cart.Get(clientId);
        return new CartDto
        {
            Id = cart.Id,
            City = cart.City,
            Country = cart.Country,
            Email = cart.Email,
            Phone = cart.Phone,
            Products = Cart.InCart(clientId).AsDto()
        };
    }
    [HttpPut("id={id}/value={value}/Properity={properity}")]
    public string update(int id, string value)
    {
        return Cart.Update(id, value);
    }
    [HttpGet("{cntById:int}")]
    public int GetCount(int cntById)
    {
        return Cart.Count(cntById);
    }
    [HttpGet("totPriceById={id:int}")]
    public decimal PriceById(int id)
    {
        return Cart.TotalPrice(id);
    }
    [HttpDelete("productId={id:int}")]
    public string Delete(int id)
    {
        return Cart.RemoveFromCart(id);
    }


}