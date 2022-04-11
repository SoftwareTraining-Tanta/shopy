

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


using Shopy.Web.Shared;
#nullable disable

namespace Shopy.Web.Models;

[Table("clients")]
public partial class Client
{
    public Client()
    {
        Carts = new HashSet<Cart>();
        Products = new HashSet<Product>();
    }

    [Key]
    [Column("username")]
    [StringLength(30)]
    public string Username { get; set; }
    [Required]
    [Column("name")]
    [StringLength(30)]
    public string Name { get; set; }
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
    [Required]
    [Column("verificationCode")]
    [StringLength(6)]
    public string VerificationCode { get; set; }
    [Required]
    [Column("password")]
    [StringLength(100)]
    public string Password { get; set; }

    [InverseProperty(nameof(Cart.ClientNavigation))]
    public virtual ICollection<Cart> Carts { get; set; }
    [InverseProperty(nameof(Product.ClientNavigation))]
    public virtual ICollection<Product> Products { get; set; }
    public enum Properties { Name, Email, Phone, City, Country, Password };
    public static string Add(Client client)
    {
        using (ShopyCtx db = new())
        {
            // ILoggerFactory loggerFactory = db.GetService<ILoggerFactory>();
            // loggerFactory.AddProvider(new ConsoleLoggerProvider());

            var getClient = db.Clients.FirstOrDefault(cl => cl.Equals(client));
            if (getClient != null)
            {
                return "Client already exists";
            }

            db.Clients.Add(client);
            client.Carts.Add(
                new Cart()
                {
                    ClientUsername = client.Username,
                    Country = client.Country,
                    City = client.City,
                    Phone = client.Phone,
                    Email = client.Email
                }
            );
            db.SaveChanges();
            return "Done adding client";
        }
    }
    public static Client Get(string username)
    {
        using (ShopyCtx db = new())
        {
            return db.Clients.FirstOrDefault(client => client.Username == username);
        }
    }
    public static bool Exist(string username)
    {
        using (ShopyCtx db = new())
        {
            Client client = db.Clients.FirstOrDefault(cl => cl.Username == username);
            return client != null;
        }
    }
    public static string Delete(string username)
    {
        var isFound = Exist(username);
        if (!isFound)
            return MyExceptions.ClientNotFound(username);
        using ShopyCtx db = new();
        var client = db.Clients.FirstOrDefault(client => client.Username == username);
        // Cart cart = db.Carts.Where(c => c.ClientId == Username).FirstOrDefault();
        // Cart.Delete(cart);
        if (client != null) db.Clients.Remove(client);
        db.SaveChanges();
        return "Client Deleted";
    }
    public static string Update(string username, dynamic value, Properties Properity = Properties.Name)
    {
        bool isFound = Exist(username);
        if (!isFound) return MyExceptions.ClientNotFound(username);
        using (ShopyCtx db = new())
        {
            var client = db.Clients.FirstOrDefault(c => c.Username == username);
            switch (Properity)
            {
                case Properties.Name:
                    if (client != null) client.Name = value;
                    break;
                case Properties.Country:
                    if (client != null) client.Country = value;
                    break;
                case Properties.City:
                    if (client != null) client.City = value;
                    break;
                case Properties.Email:
                    if (client != null) client.Email = value;
                    break;
                case Properties.Phone:
                    if (client != null) client.Phone = value;
                    break;
                case Properties.Password:
                    if (client != null) client.Password = value;
                    break;
                default:
                    if (client != null) client.Name = value;
                    break;
            }
            db.SaveChanges();
            return "Updated";
        }
    }
    public static void UpdateVerificationCode(Client client, string verificationCode)
    {
        using (ShopyCtx db = new())
        {
            client.VerificationCode = verificationCode;
            db.SaveChanges();
        }
    }
    public static List<Client> AllClients(int limit)
    {
        using (ShopyCtx db = new())
        {
            var clients = db.Clients.Take(limit).DefaultIfEmpty().ToList();
            if (clients != null) return clients;
            return new List<Client>();
        }
    }
    public static List<Product> ClientProducts(string Username) // previously bought products
    {
        using (ShopyCtx db = new())
        {
            // ILoggerFactory loggerFactory = db.GetService<ILoggerFactory>();
            // loggerFactory.AddProvider(new ConsoleLoggerProvider());

            // if the product is in the cart, It will not be in this list.

            var products = db.Products.Where(p => p.ClientUsername == Username && p.CartId == null).ToList();
            if (products != null) return products;
            return new List<Product>();
            // return client.Products.Where(p => p.CartId == null).ToList(); // use join

        }
    }
}
