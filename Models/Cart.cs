using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Microsoft.EntityFrameworkCore;

using Shopy.Models.Shared;
using Shopy.Models.Dtos;
#nullable disable

namespace Shopy.Models
{
    [Table("carts")]
    [Index(nameof(ClientId), Name = "clientId")]
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
        [Column("clientId")]
        public Nullable<int> ClientId { get; set; }
        [Required]
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

        [ForeignKey(nameof(ClientId))]
        [InverseProperty("Carts")]
        public virtual Client Client { get; set; }
        [InverseProperty(nameof(Product.Cart))]
        public virtual ICollection<Product> Products { get; set; }

        public enum Properties { Email, Phone, City, Country };

        public static string Update(int clientId, dynamic value, Properties properity = Properties.City)
        {
            bool isFound = Exist(clientId);
            if (!isFound) return MyExceptions.ClientNotFound(clientId);
            using (ShopyCtx db = new())
            {
                Cart cart = Cart.Get(clientId);
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

        private static bool Exist(int clientId)
        {
            using (ShopyCtx db = new())
            {
                var cart = db.Carts.FirstOrDefault(c => c.ClientId == clientId);
                return cart != null;
            }
        }
        public static dynamic Get(int clientId)
        {
            using (ShopyCtx db = new())
            {
                Cart cart = db.Carts.FirstOrDefault(cart => cart.ClientId == clientId);
                if (cart == null)
                {
                    return MyExceptions.ClientNotFound(clientId);
                }
                return cart;
            }
        }
        public static int Count(int clientId)
        {
            bool isFound = Cart.Exist(clientId);
            if (!isFound)
                return 0;
            using (ShopyCtx db = new())
            {
                Cart cart = db.Carts.FirstOrDefault(c => c.ClientId == clientId);
                if (cart == null)
                {
                    return 0;
                }
                return db.Products.Where(p => p.CartId == cart.Id).Count();
            }
        }

        public static string AddToCart(int ClientId, int ProductId) // added by Harby at 12:00 AM
        {
            using (ShopyCtx db = new())
            {
                var cartId = db.Carts.Where(c => c.ClientId == ClientId).FirstOrDefault().Id;
                Product.Update(ProductId, ClientId, Product.Properities.ClientId);
                Product.Update(ProductId, cartId, Product.Properities.CartId);
                return "successful Transactions";
            }
        }

        public static decimal TotalPrice(int clientId)
        {
            if (!Exist(clientId))
            {
                return 0;
            }
            using (ShopyCtx db = new())
            {
                return db.Products.Where(p => p.ClientId == clientId).Sum(p => p.Price);
            }
        }

        public static string RemoveFromCart(int ProductId)
        {
            using (ShopyCtx db = new())
            {
                Product.Update(ProductId, null, Product.Properities.ClientId);
                Product.Update(ProductId, null, Product.Properities.CartId);
                return "Item is removed from cart";
            }
        }
        public static string CheckOut(int clientId)
        {
            using (ShopyCtx db = new())
            {
                var cart = db.Carts.FirstOrDefault(c => c.ClientId == clientId);
                if (cart == null)
                {
                    // No exceptions for the client, no cart -> no client
                    return MyExceptions.ClientNotFound(clientId);
                }
                var cartProducs = cart.Products.ToList();
                foreach (Product p in cartProducs)
                {
                    Product.Update(p.Id, null, Product.Properities.CartId);
                }
                return "successful Transactions";
            }
        }

        public static dynamic InCart(int clientId)
        {

            using (ShopyCtx db = new())
            {
                int cartId = Cart.Get(clientId).Id;
                var products = db.Products.Where(c => c.ClientId == clientId && c.CartId == cartId).DefaultIfEmpty().ToList();
                if (products == null)
                {
                    return "Failed operation";
                }
                return products;
            }
        }
    }
}
