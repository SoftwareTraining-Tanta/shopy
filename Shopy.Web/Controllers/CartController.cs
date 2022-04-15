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
    [HttpPost("add/{customerUsername}/{modelName}")]
    public ActionResult Add(string customerUsername, string modelName)
    {
        Client client = new();
        Product product = new();
        if (!client.Exist(customerUsername))
            return BadRequest(MyExceptions.ClientNotFound(customerUsername));

        try
        {
            Cart Cart = new();
            Cart.AddToCart(customerUsername, modelName);
            return Ok("Done adding to cart");
        }
        catch (Exception ex)
        {
            return BadRequest("Error adding to cart: " + ex.Message);
        }
    }
    [HttpGet("get/{clientUsername}")]
    public ActionResult Get(string clientUsername)
    {
        Client client = new();
        if (!client.Exist(clientUsername))
        {
            return BadRequest(MyExceptions.ClientNotFound(clientUsername));
        }
        try
        {

            Cart Cart = new();
            CartDto cart = Cart.Get(clientUsername).AsDto();
            return Ok(cart);
        }
        catch (Exception ex)
        {
            return BadRequest("Error getting cart " + ex.Message);
        }
    }
    [HttpPut("updateCity/id={id}/value={value}")]
    public ActionResult Update(string clientUsername, string value)
    {
        Client client = new();
        if (!client.Exist(clientUsername))
        {
            return BadRequest(MyExceptions.ClientNotFound(clientUsername));
        }
        try
        {
            Cart Cart = new();
            Cart.UpdateCity(clientUsername, value);
            return Ok("Done updating cart");
        }
        catch (Exception ex)
        {
            return BadRequest("Error updating cart: " + ex.Message);
        }
    }

    [HttpGet("count/{clientUsername}")]
    public ActionResult GetCount(string clientUsername)
    {
        Client client = new();
        if (!client.Exist(clientUsername))
        {
            return BadRequest(MyExceptions.ClientNotFound(clientUsername));
        }
        try
        {
            Cart Cart = new();
            return Ok(Cart.Count(clientUsername));
        }
        catch (Exception ex)
        {
            return BadRequest("Error getting cart count: " + ex.Message);
        }
    }
    [HttpGet("totalprice/{clientUsername}")]
    public ActionResult PriceById(string clientUsername)
    {
        Client client = new();
        if (!client.Exist(clientUsername))
        {
            return BadRequest(MyExceptions.ClientNotFound(clientUsername));
        }
        try
        {
            Cart Cart = new();
            decimal totalPrice = Cart.TotalPrice(clientUsername);
            return Ok(totalPrice);
        }
        catch (Exception ex)
        {
            return BadRequest("Error getting total price: " + ex.Message);
        }
    }
    [HttpDelete("delete/{id:int}")]
    public ActionResult Delete(int id)
    {
        Product product = new();
        if (!product.Exist(id))
        {
            return BadRequest("Product you want to remove from cart does not exist");
        }
        try
        {
            Cart Cart = new();
            Cart.RemoveFromCart(id);
            return Ok("Done deleting from cart");
        }
        catch (Exception ex)
        {
            return BadRequest("Error removing from cart: " + ex.Message);
        }
    }
    [HttpGet("InCart/{clientUsername}")]
    public ActionResult InCart(string clientUsername)
    {
        Client client = new();
        if (!client.Exist(clientUsername))
        {
            return BadRequest(MyExceptions.ClientNotFound(clientUsername));
        }
        try
        {
            Cart Cart = new();
            List<ProductDto> products = Cart.InCart(clientUsername).AsDto();
            return Ok(products);
        }
        catch (Exception ex)
        {
            return BadRequest("Error getting products in cart: " + ex.Message);
        }
    }
    [HttpPut("checkout/{clientUsername}")]
    public ActionResult CheckOut(string clientUsername)
    {
        Client client = new();
        if (!client.Exist(clientUsername))
        {
            return BadRequest(MyExceptions.ClientNotFound(clientUsername));
        }
        try
        {
            Cart Cart = new();
            Cart.CheckOut(clientUsername);
            return Ok("Done checking out");
        }
        catch (Exception ex)
        {
            return BadRequest("Error checking out: " + ex.Message);
        }
    }
    [HttpDelete("RemoveFromCart/{pid:int}")]
    public ActionResult RemoveFromCart(int pid)
    {
        Product product = new();
        if (!product.Exist(pid))
        {
            return BadRequest("Product you want to remove from cart does not exist");
        }
        try
        {
            Cart Cart = new();
            Cart.RemoveFromCart(pid);
            return Ok("Done removing from cart");
        }
        catch (Exception ex)
        {
            return BadRequest("Error removing from cart: " + ex.Message);
        }
    }
}