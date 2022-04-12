using Microsoft.AspNetCore.Mvc;
namespace Northwind.WebApi.Controllers;

using Shopy.Web.Models;
using Shopy.Web.Dtos;
using Shopy.Web.Shared;
using Shopy.Web.Interfaces;

[ApiController]
[Route("api/carts/")]
public class CartController : ControllerBase
{
    [HttpPost("add/{customerUsername}/{pid:int}")]
    public ActionResult Add(string customerUsername, int pid)
    {
        try
        {
            Cart Cart = new();
            Cart.AddToCart(customerUsername, pid);
            return Ok("Done adding to cart");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpGet("getcart/{clientUsername}")]
    public CartDto Get(string clientUsername)
    {
        Cart Cart = new();
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
    public void update(string clientUsername, string value)
    {
        Cart Cart = new();

        Cart.Update(clientUsername, value);
    }
    [HttpGet("count/{clientUsername}")]
    public int GetCount(string clientUsername)
    {
        Cart Cart = new();
        return Cart.Count(clientUsername);
    }
    [HttpGet("totalprice/{clientUsername}")]
    public decimal PriceById(string clientUsername)
    {
        Cart Cart = new();

        return Cart.TotalPrice(clientUsername);
    }
    [HttpDelete("delete/{id:int}")]
    public string Delete(int id)
    {
        Cart Cart = new();

        return Cart.RemoveFromCart(id);
    }


}