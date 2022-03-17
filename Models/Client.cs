using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Logging;
using Models.shared;
#nullable disable

namespace Models
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


        public enum Properities { Name, Id, Email, Phone, City, Country, Password };
        public static string Add(Client client)
        {

            using (Shopy db = new())
            {
                ILoggerFactory loggerFactory = db.GetService<ILoggerFactory>();
                loggerFactory.AddProvider(new ConsoleLoggerProvider());

                var getClient = db.Clients.Where(cl => cl.Equals(client)).FirstOrDefault();
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
            using (Shopy db = new())
            {
                return db.Clients.Where(client => client.Id == id).FirstOrDefault();
            }

        }
        private static bool Exist(int id)
        {
            using (Shopy db = new())
            {
                Client client = db.Clients.Where(client => client.Id == id).FirstOrDefault();
                return client != null;
            }
        }
        public static dynamic Delete(int id)
        {
            bool isFound = Exist(id);
            if (!isFound)
                return MyExceptions.ClientNotFound(id);
            using (Shopy db = new())
            {
                Client client = db.Clients.Where(client => client.Id == id).FirstOrDefault();
                db.Clients.Remove(client);
                db.SaveChanges();
                return "Client Deleted";
            }
        }
        public static string Update(int id, dynamic value, Properities properities = Properities.Name)
        {
            bool isFound = Exist(id);
            if (!isFound) return MyExceptions.ClientNotFound(id);
            using (Shopy db = new())
            {
                Client client = db.Clients.Where(c => c.Id == id).FirstOrDefault();
                switch (properities)
                {
                    case Properities.Name:
                        client.Name = value;
                        break;
                    case Properities.Country:
                        client.Country = value;
                        break;
                    case Properities.City:
                        client.City = value;
                        break;
                    case Properities.Email:
                        client.Email = value;
                        break;
                    case Properities.Phone:
                        client.Phone = value;
                        break;
                    case Properities.Password:
                        client.Password = value;
                        break;
                    default:
                        client.Name = value;
                        break;
                }
                db.SaveChanges();
                return "Updated";
            }
        }
        public static List<Client> AllClients()
        {
            using (Shopy db = new())
            {
                List<Client> clients = db.Clients.DefaultIfEmpty().ToList();
                return clients;
            }
        }
        public static List<Product> ClientProducts(int id)
        {
            using (Shopy db = new())
            {
                // ILoggerFactory loggerFactory = db.GetService<ILoggerFactory>();
                // loggerFactory.AddProvider(new ConsoleLoggerProvider());

                // if the product is in the cart, It will not be in this list.
                Client client = Get(id);
                return client.Products.Where(p => p.CartId == null).ToList();
            }
        }
        
    }
}
