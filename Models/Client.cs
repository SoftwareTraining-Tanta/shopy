using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
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
                try
                {
                    db.Clients.Add(client);
                    db.SaveChanges();
                    return "Done adding client";
                }
                catch
                {
                    return "Client is already inserted";
                }
            }
        }
        public static dynamic Get(int id)
        {
            bool isFound = Check(id);
            using (Shopy db = new())
            {
                try
                {
                    if (isFound)
                    {
                        Client client = db.Clients.Where(client => client.Id == id).First();
                        return client;
                    }
                    return MyExceptions.ClientNotFound(id);
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }

        }
        private static bool Check(int id)
        {
            using (Shopy db = new())
            {
                try
                {
                    Client client = db.Clients.Where(client => client.Id == id).First();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }
        public static dynamic Delete(int id)
        {
            bool isFound = Check(id);
            using (Shopy db = new())
            {
                if (isFound)
                {
                    Client client = db.Clients.Where(client => client.Id == id).First();
                    db.Clients.Remove(client);
                    db.SaveChanges();
                    return client;
                }
                return MyExceptions.ClientNotFound(id);
            }
        }
        public static void Update(int id, dynamic value, Properities properities = Properities.Name)
        {

            using (Shopy db = new())
            {
                Client client = db.Clients.Where(c => c.Id == id).First();
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
                    default:
                        client.Name = value;
                        break;
                }
                db.SaveChanges();
            }
        }
    }
}
