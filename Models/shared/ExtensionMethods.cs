using System.Collections.Generic;
using Shopy.Models.Dtos;
namespace Shopy.Models.Shared;
public static class ExtensionMethods
{
    public static ClientDto AsDto(this Client client)
    {
        return new ClientDto
        {
            Id = client.Id,
            Name = client.Name,
            Country = client.Country,
            City = client.City,
            Email = client.Email,
            Password = client.Password,
            Phone = client.Phone,
        };
    }
    public static CartDto AsDto(this Cart cart)
    {
        return new CartDto
        {
            Id = cart.Id,
            City = cart.City,
            Country = cart.Country,
            Email = cart.Email,
            Phone = cart.Phone,
            Products = cart.Products.AsDto(),
        };
    }
    public static ProductDto AsDto(this Product product)
    {
        return new ProductDto
        {
            Category = product.Category,
            Details = product.Details,
            Id = product.Id,
            ImagePath = product.ImagePath,
            Model = product.Model,
            Price = product.Price
        };
    }
    public static ICollection<ProductDto> AsDto(this ICollection<Product> products)
    {
        List<ProductDto> productDtos = new();
        foreach (Product p in products)
        {
            productDtos.Add(p.AsDto());
        }
        return productDtos;
    }
    public static ICollection<CartDto> AsDto(this ICollection<Cart> carts)
    {
        List<CartDto> cartDtos = new();
        foreach (Cart c in carts)
        {
            cartDtos.Add(c.AsDto());
        }
        return cartDtos;
    }
    public static List<ClientDto> AsDto(this ICollection<Client> clients)
    {
        List<ClientDto> clientDtos = new();
        foreach (Client c in clients)
        {
            clientDtos.Add(c.AsDto());
        }
        return clientDtos;
    }
}