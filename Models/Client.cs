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


        public enum Properities { Name, Email, Phone, City, Country, Password };
        public static string Add(Client Client)
        {

            using (ShopyCtx db = new())
            {
                ILoggerFactory loggerFactory = db.GetService<ILoggerFactory>();
                loggerFactory.AddProvider(new ConsoleLoggerProvider());

                var getClient = db.Clients.Where(cl => cl.Equals(Client)).FirstOrDefault();
                if (getClient != null)
                {
                    return "Client already exists";
                }

                db.Clients.Add(Client);
                Client.Carts.Add(
                    new Cart()
                    {
                        ClientId = Client.Id,
                        Country = Client.Country,
                        City = Client.City,
                        Phone = Client.Phone,
                        Email = Client.Email
                    }
                );
                db.SaveChanges();
                return "Done adding client";
            }
        }
        public static dynamic Get(int Id)
        {
            bool isFound = Exist(Id);
            if (!isFound)
            {
                return MyExceptions.ClientNotFound(Id);
            }
            using (ShopyCtx db = new())
            {
                return db.Clients.Where(client => client.Id == Id).FirstOrDefault();
            }

        }
        private static bool Exist(int Id)
        {
            using (ShopyCtx db = new())
            {
                Client client = db.Clients.Where(client => client.Id == Id).FirstOrDefault();
                return client != null;
            }
        }
        public static dynamic Delete(int Id)
        {
            if (Id == 1)
                return MyExceptions.InvalidDeletion(Id);
            bool isFound = Exist(Id);
            if (!isFound)
                return MyExceptions.ClientNotFound(Id);
            using (ShopyCtx db = new())
            {
                Client client = db.Clients.Where(client => client.Id == Id).FirstOrDefault();
                // Cart cart = db.Carts.Where(c => c.ClientId == Id).FirstOrDefault();
                // Cart.Delete(cart);
                db.Clients.Remove(client);
                db.SaveChanges();
                return "Client Deleted";
            }
        }
        public static string Update(int Id, dynamic Value, Properities Properities = Properities.Name)
        {
            bool isFound = Exist(Id);
            if (!isFound) return MyExceptions.ClientNotFound(Id);
            using (ShopyCtx db = new())
            {
                Client client = db.Clients.Where(c => c.Id == Id).FirstOrDefault();
                switch (Properities)
                {
                    case Properities.Name:
                        client.Name = Value;
                        break;
                    case Properities.Country:
                        client.Country = Value;
                        break;
                    case Properities.City:
                        client.City = Value;
                        break;
                    case Properities.Email:
                        client.Email = Value;
                        break;
                    case Properities.Phone:
                        client.Phone = Value;
                        break;
                    case Properities.Password:
                        client.Password = Value;
                        break;
                    default:
                        client.Name = Value;
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
                List<Client> clients = db.Clients.DefaultIfEmpty().ToList();
                return clients;
            }
        }
        public static List<Product> ClientProducts(int ClientId)
        {
            using (ShopyCtx db = new())
            {
                // ILoggerFactory loggerFactory = db.GetService<ILoggerFactory>();
                // loggerFactory.AddProvider(new ConsoleLoggerProvider());

                // if the product is in the cart, It will not be in this list.
                Client client = Get(ClientId);
                return client.Products.Where(p => p.CartId == 1).ToList(); // use join
            }
        }
    }
}
