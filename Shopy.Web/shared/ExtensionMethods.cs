using Shopy.Web.Models;
using Shopy.Web.Dtos;
using System.Security.Cryptography;
using System.Text;

namespace Shopy.Web.Shared;
public static class ExtensionMethods
{
    public static ClientDto AsDto(this Client client)
    {
        return new ClientDto
        {
            Username = client.Username,
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
            Model = product.Model,
            VendorUsername = product.GetVendor(product.Id).Username,
        };
    }
    public static List<ProductDto> AsDto(this ICollection<Product> products)
    {
        List<ProductDto> productDtos = new();
        foreach (Product p in products)
        {
            productDtos.Add(p.AsDto());
        }
        return productDtos;
    }
    public static List<CartDto> AsDto(this ICollection<Cart> carts)
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
    public static Client AsNormal(this ClientDto clientDto)
    {
        return new Client
        {
            Name = clientDto.Name,
            Country = clientDto.Country,
            City = clientDto.City,
            Email = clientDto.Email,
            Password = clientDto.Password,
            Phone = clientDto.Phone,
            Username = clientDto.Username
        };
    }
    public static string ToSha256(this string text)
    {
        using (HashAlgorithm algorithm = SHA256.Create())
        {
            byte[] hashedText = algorithm.ComputeHash(Encoding.UTF8.GetBytes(text));
            StringBuilder stringBuilder = new();
            foreach (byte b in hashedText)
            {
                stringBuilder.Append(b.ToString("X2"));
            }
            return stringBuilder.ToString();
        }
    }

}