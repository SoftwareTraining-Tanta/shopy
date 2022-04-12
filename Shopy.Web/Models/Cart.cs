
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Shopy.Web.Shared;
using Shopy.Web.Interfaces;
#nullable disable

namespace Shopy.Web.Models;

[Table("carts")]
[Index(nameof(ClientUsername), Name = "clientUsername")]
public partial class Cart : ICart
{
    public Cart()
    {
        Products = new HashSet<Product>();
    }

    [Key]
    [Column("id")]
    public int Id { get; set; }
    [Required]
    [Column("clientUsername")]
    [StringLength(30)]
    public string ClientUsername { get; set; }
    [Column("country")]
    [StringLength(30)]
    public string Country { get; set; }
    [Column("city")]
    [StringLength(30)]
    public string City { get; set; }
    [Required]
    [Column("phone")]
    [StringLength(20)]
    public string Phone { get; set; }
    [Required]
    [Column("email")]
    [StringLength(60)]
    public string Email { get; set; }

    [ForeignKey(nameof(ClientUsername))]
    [InverseProperty(nameof(Client.Carts))]
    public virtual Client ClientNavigation { get; set; }
    [InverseProperty(nameof(Product.Cart))]
    public virtual ICollection<Product> Products { get; set; }

    public enum Properties { Email, Phone, City, Country };

    public string Update(string clientUsername, dynamic value)
    {
        bool isFound = Exist(clientUsername);
        if (!isFound) return MyExceptions.ClientNotFound(clientUsername);
        using (ShopyCtx db = new())
        {
            Cart Cart = new();
            var cart = Cart.Get(clientUsername);
            cart.City = value;
            db.SaveChanges();
            return "Done";
        }
    }

    public bool Exist(string clientUsername)
    {
        using (ShopyCtx db = new())
        {
            var cart = db.Carts.FirstOrDefault(c => c.ClientUsername == clientUsername);
            return cart != null;
        }
    }
    public dynamic Get(string clientUsername)
    {
        using (ShopyCtx db = new())
        {
            var cart = db.Carts.FirstOrDefault(cart => cart.ClientUsername == clientUsername);
            if (cart == null)
            {
                return MyExceptions.ClientNotFound(clientUsername);
            }
            return cart;
        }
    }
    public int Count(string clientUsername)
    {
        Cart Cart = new();

        bool isFound = Cart.Exist(clientUsername);
        if (!isFound)
            return 0;
        using (ShopyCtx db = new())
        {
            var cart = db.Carts.FirstOrDefault(c => c.ClientUsername == clientUsername);
            if (cart == null)
            {
                return 0;
            }
            return db.Products.Where(p => p.CartId == cart.Id).Count();
        }
    }

    public void AddToCart(string ClientUsername, int ProductId) // added by Harby at 12:00 AM
    {
        using (ShopyCtx db = new())
        {
            Product Product = new();
            var cartId = db.Carts.FirstOrDefault(c => c.ClientUsername == ClientUsername).Id;
            Product.Update(ProductId, ClientUsername, Product.Properities.ClientUsername);
            Product.Update(ProductId, cartId, Product.Properities.CartId);
        }
    }

    public decimal TotalPrice(string clientUsername)
    {
        if (!Exist(clientUsername))
        {
            return 0;
        }
        using (ShopyCtx db = new())
        {
            return db.Products.Where(p => p.ClientUsername == clientUsername).Include(p => p.ModelNavigation).Sum(p => p.ModelNavigation.Price);
        }
    }

    public string RemoveFromCart(int ProductId)
    {
        using (ShopyCtx db = new())
        {
            Product Product = new();

            Product.Update(ProductId, null, Product.Properities.ClientUsername);
            Product.Update(ProductId, null, Product.Properities.CartId);
            return "Item is remove from cart";
        }
    }
    public string CheckOut(string clientUsername)
    {
        using (ShopyCtx db = new())
        {
            var cart = db.Carts.FirstOrDefault(c => c.ClientUsername == clientUsername);
            if (cart == null)
            {
                // No exceptions for the client, no cart -> no client
                return MyExceptions.ClientNotFound(clientUsername);
            }
            var cartProducs = cart.Products.ToList();
            foreach (Product p in cartProducs)
            {
                Product Product = new();

                Product.Update(p.Id, null, Product.Properities.CartId);
            }
            return "successful Transactions";
        }
    }

    public List<Product> InCart(string clientUsername)
    {

        using (ShopyCtx db = new())
        {
            var cart = db.Carts.FirstOrDefault(c => c.ClientUsername == clientUsername);
            if (cart == null)
            {
                return new List<Product>();
            }
            return cart.Products.ToList();
        }
    }




}
