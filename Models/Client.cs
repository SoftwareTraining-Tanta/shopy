using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Logging;
using Shopy.Models.shared;
#nullable disable

namespace Shopy.Models
{
    [Table("clients")]
    public partial class Client
    {
        public Client()
        {
            Carts = new HashSet<Cart>();
            Products = new HashSet<Product>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("name")]
        [StringLength(30, ErrorMessage = "Name Can't be more than 30 character length")]
        public string Name { get; set; }
        [Column("country")]
        [StringLength(30, ErrorMessage = "Country Can't be more than 30 character length")]
        public string Country { get; set; }
        [Column("city")]
        [StringLength(30, ErrorMessage = "City Can't be more than 30 character length")]
        public string City { get; set; }
        [Required]
        [Column("phone")]
        [StringLength(20, ErrorMessage = "Phone number Can't be more than 20 character length")]
        public string Phone { get; set; }
        [Required]
        [Column("email")]
        [StringLength(60, ErrorMessage = "Email Can't be more than 60 character length")]
        public string Email { get; set; }
        [Required]
        [Column("password")]
        [StringLength(100)]
        public string Password { get; set; }

        [InverseProperty(nameof(Cart.Client))]
        public virtual ICollection<Cart> Carts { get; set; }
        [InverseProperty(nameof(Product.Client))]
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
                        ClientId = client.Id,
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
        public static dynamic Get(int id)
        {
            bool isFound = Exist(id);
            if (!isFound)
            {
                return MyExceptions.ClientNotFound(id);
            }
            using (ShopyCtx db = new())
            {
                return db.Clients.FirstOrDefault(client => client.Id == id);
            }
        }
        private static bool Exist(int id)
        {
            using ShopyCtx db = new();
            var client = db.Clients.FirstOrDefault(client => client.Id == id);
            return client != null;
        }
        public static dynamic Delete(int id)
        {
            var isFound = Exist(id);
            if (!isFound)
                return MyExceptions.ClientNotFound(id);
            using ShopyCtx db = new();
            var client = db.Clients.FirstOrDefault(client => client.Id == id);
            // Cart cart = db.Carts.Where(c => c.ClientId == Id).FirstOrDefault();
            // Cart.Delete(cart);
            if (client != null) db.Clients.Remove(client);
            db.SaveChanges();
            return "Client Deleted";
        }
        public static string Update(int id, dynamic value, Properties Properity = Properties.Name)
        {
            bool isFound = Exist(id);
            if (!isFound) return MyExceptions.ClientNotFound(id);
            using (ShopyCtx db = new())
            {
                var client = db.Clients.FirstOrDefault(c => c.Id == id);
                switch (Properity)
                {
                    case Properties.Name:
                        if(client != null) client.Name = value;
                        break;
                    case Properties.Country:
                        if(client != null) client.Country = value;
                        break;
                    case Properties.City:
                        if(client != null) client.City = value;
                        break;
                    case Properties.Email:
                        if(client != null) client.Email = value;
                        break;
                    case Properties.Phone:
                        if(client != null) client.Phone = value;
                        break;
                    case Properties.Password:
                        if(client != null) client.Password = value;
                        break;
                    default:
                        if(client != null) client.Name = value;
                        break;
                }
                db.SaveChanges();
                return "Updated";
            }
        }
        public static List<Client> AllClients()
        {
            using (ShopyCtx db = new())
            {
                var clients = db.Clients.DefaultIfEmpty().ToList();
                if(clients != null) return clients;
                return new List<Client>();
            }
        }
        public static List<Product> ClientProducts(int ClientId) // previously bought products
        {
            using (ShopyCtx db = new())
            {
                // ILoggerFactory loggerFactory = db.GetService<ILoggerFactory>();
                // loggerFactory.AddProvider(new ConsoleLoggerProvider());

                // if the product is in the cart, It will not be in this list.
        
                var products = db.Products.Where(p => p.ClientId == ClientId && p.CartId == null).ToList();
                if (products != null) return products;
                return new List<Product>();
                // return client.Products.Where(p => p.CartId == null).ToList(); // use join

            }
        }
    }
}
