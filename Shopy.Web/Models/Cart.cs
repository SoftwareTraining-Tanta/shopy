
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Shopy.Web.Shared;
#nullable disable

namespace Shopy.Web.Models;

[Table("carts")]
[Index(nameof(ClientUsername), Name = "clientUsername")]
public partial class Cart
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

    public static string Update(string clientUsername, dynamic value, Properties properity = Properties.Phone)
    {
        bool isFound = Exist(clientUsername);
        if (!isFound) return MyExceptions.ClientNotFound(clientUsername);
        using (ShopyCtx db = new())
        {
            var cart = Cart.Get(clientUsername);
            switch (properity)
            {
                case Properties.Country:
                    if (cart != null) cart.Country = value;
                    break;
                case Properties.City:
                    if (cart != null) cart.City = value;
                    break;
                case Properties.Email:
                    if (cart != null) cart.Email = value;
                    break;
                case Properties.Phone:
                    if (cart != null) cart.Phone = value;
                    break;
            }
            db.SaveChanges();
            return "Updated";
        }
    }

    private static bool Exist(string clientUsername)
    {
        using (ShopyCtx db = new())
        {
            var cart = db.Carts.FirstOrDefault(c => c.ClientUsername == clientUsername);
            return cart != null;
        }
    }
    public static dynamic Get(string clientUsername)
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
    public static int Count(string clientUsername)
    {
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

    public static string AddToCart(string ClientUsername, int ProductId) // added by Harby at 12:00 AM
    {
        using (ShopyCtx db = new())
        {
            var cartId = db.Carts.FirstOrDefault(c => c.ClientUsername == ClientUsername).Id;
            Product.Update(ProductId, ClientUsername, Product.Properities.ClientUsername);
            Product.Update(ProductId, cartId, Product.Properities.CartId);
            return "successful Transactions";
        }
    }

    public static decimal TotalPrice(string clientUsername)
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

    public static string RemoveFromCart(int ProductId)
    {
        using (ShopyCtx db = new())
        {
            Product.Update(ProductId, null, Product.Properities.ClientUsername);
            Product.Update(ProductId, null, Product.Properities.CartId);
            return "Item is remove from cart";
        }
    }
    public static string CheckOut(string clientUsername)
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
                Product.Update(p.Id, null, Product.Properities.CartId);
            }
            return "successful Transactions";
        }
    }

    public static List<Product> InCart(string clientUsername)
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
