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
        public int ClientId { get; set; }
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

        public static string Update(int id, dynamic value, Properties properity)
        {
            bool isFound = Exist(id);
            if (!isFound) return MyExceptions.CartNotFound(id);
            using (ShopyCtx db = new())
            {
                var cart = db.Carts.FirstOrDefault(c => c.Id == id);
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

        private static bool Exist(int curtId)
        {
            using (ShopyCtx db = new())
            {
                Cart cart = db.Carts.FirstOrDefault(c => c.Id == curtId);
                return cart != null;
            }
        }
        public static dynamic Get(int cartId)
        {
            bool isFound = Exist(cartId);
            if (!isFound)
            {
                return MyExceptions.CartNotFound(cartId);
            }
            using (ShopyCtx db = new())
            {
                return db.Carts.FirstOrDefault(cart => cart.Id == cartId);
            }
        }
        public static int Count(int cartId)
        {
            bool isFound = Exist(cartId);
            if (!isFound)
                return 0;
            using (ShopyCtx db = new())
            {
                return db.Products.Where(p => p.CartId == cartId).Count();
            }
        }

        public static string AddToCart(int ClientId, int ProductId) // added by Harby at 12:00 AM
        {
            using (ShopyCtx db = new())
            {
                var cartId = db.Carts.FirstOrDefault(c => c.ClientId.Equals(ClientId)).Id;
                Product.Update(ProductId, ClientId, Product.Properities.ClientId);
                Product.Update(ProductId, cartId, Product.Properities.CartId);
                return "successful Transactions";
            }
        }
        public static string RemoveFromCart(int ProductId)
        {
            using (ShopyCtx db = new())
            {
                Product.Update(ProductId, null, Product.Properities.ClientId);
                Product.Update(ProductId, null, Product.Properities.CartId);
                return "Item is remove from cart";
            }
        }
        public static string CheckOut(int cartId)
        {
            using (ShopyCtx db = new())
            {
                var cart = Get(cartId);
                var cartProducs = cart.Products.ToList();
                foreach (Product p in cartProducs)
                {
                    Product.Update(p.Id, null, Product.Properities.CartId);
                }
                return "successful Transactions";
            }
        }
        // :: TODO
        // add cart total price
        public static List<Product> InCart(int cartId)
        {

            using (ShopyCtx db = new())
            {
                Cart cart = Get(cartId);
                return cart.Products.ToList();
            }
        }
        public static int CountInCart(int cartId)
        {
            using (ShopyCtx db = new())
            {
                Cart cart = Get(cartId);
                return cart.Products.Count();
            }
        }
    }
}
