
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

    public void UpdateCity(string clientUsername, string value)
    {
        using (ShopyCtx db = new())
        {
            Cart cart = db.Carts.FirstOrDefault(c => c.ClientUsername == clientUsername);
            cart.City = value;
            db.SaveChanges();
        }
    }
    public void UpdatePhone(string clientUsername, string value)
    {
        using (ShopyCtx db = new())
        {
            Cart cart = db.Carts.FirstOrDefault(c => c.ClientUsername == clientUsername);
            cart.Phone = value;
            db.SaveChanges();
        }
    }

    public bool Exist(string clientUsername)
    {
        using (ShopyCtx db = new())
        {
            Cart cart = db.Carts.FirstOrDefault(c => c.ClientUsername == clientUsername);
            return cart != null;
        }
    }
    public Cart Get(string clientUsername)
    {
        using (ShopyCtx db = new())
        {
            Cart cart = db.Carts.FirstOrDefault(cart => cart.ClientUsername == clientUsername);
            return cart;
        }
    }
    public int Count(string clientUsername)
    {
        Cart Cart = new();

        using (ShopyCtx db = new())
        {
            Cart cart = db.Carts.FirstOrDefault(c => c.ClientUsername == clientUsername);
            return db.Products.Where(p => p.CartId == cart.Id).Count();
        }
    }

    public void AddToCart(string ClientUsername, int ProductId) // added by Harby at 12:00 AM
    {
        using (ShopyCtx db = new())
        {
            Product Product = new();
            int cartId = db.Carts.FirstOrDefault(c => c.ClientUsername == ClientUsername).Id;
            Product.UpdateClientUsername(ProductId, ClientUsername);
            Product.UpdateCartId(ProductId, cartId);
        }
    }

    public decimal TotalPrice(string clientUsername)
    {
        using (ShopyCtx db = new())
        {
            return db.Products
            .Where(p => p.ClientUsername == clientUsername)
            .Include(p => p.ModelNavigation)
            .Sum(p => p.ModelNavigation.Price);
        }
    }

    public string RemoveFromCart(int ProductId)
    {
        using (ShopyCtx db = new())
        {
            Product Product = new();

            Product.UpdateClientUsername(ProductId, null);
            Product.UpdateCartId(ProductId, null);
            return "Item is remove from cart";
        }
    }
    public string CheckOut(string clientUsername)
    {
        List<Product> cartProducts = new();
        using (ShopyCtx db = new())
        {
            Cart cart = db.Carts.Include(c => c.Products).FirstOrDefault(c => c.ClientUsername == clientUsername);
            cartProducts = cart.Products.ToList();

        }
        foreach (Product p in cartProducts)
        {
            Product Product = new();
            Product.UpdateCartId(p.Id, null);
        }
        return "successful Transactions";

    }

    public List<Product> InCart(string clientUsername)
    {

        using (ShopyCtx db = new())
        {
            Cart cart = db.Carts.Include(c => c.Products).FirstOrDefault(c => c.ClientUsername == clientUsername);
            return cart.Products.ToList();
        }
    }




}
