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

        public enum Properities { Email, Phone, City, Country };

        public static string Update(int id, dynamic value, Properities properities)
        {
            bool isFound = Exist(id);
            if (!isFound) return MyExceptions.CartNotFound(id);
            using (ShopyCtx db = new())
            {
                Cart cart = db.Carts.Where(c => c.Id == id).FirstOrDefault();
                switch (properities)
                {
                    case Properities.Country:
                        cart.Country = value;
                        break;
                    case Properities.City:
                        cart.City = value;
                        break;
                    case Properities.Email:
                        cart.Email = value;
                        break;
                    case Properities.Phone:
                        cart.Phone = value;
                        break;
                }
                db.SaveChanges();
                return "Updated";
            }
        }

        private static bool Exist(int CurtId)
        {
            using (ShopyCtx db = new())
            {
                Cart cart = db.Carts.Where(c => c.Id == CurtId).FirstOrDefault();
                return cart != null;
            }
        }
        private static dynamic Get(int CartId)
        {
            bool isFound = Exist(CartId);
            if (!isFound)
            {
                return MyExceptions.CartNotFound(CartId);
            }
            using (ShopyCtx db = new())
            {
                return db.Carts.Where(cart => cart.Id == CartId).FirstOrDefault();
            }
        }
        public static dynamic Delete(int CartId)
        {
            bool isFound = Exist(CartId);
            if (!isFound)
                return MyExceptions.CartNotFound(CartId);
            using (ShopyCtx db = new())
            {
                Cart cart = db.Carts.Where(client => client.Id == CartId).FirstOrDefault();
                db.Carts.Remove(cart);
                db.SaveChanges();
                return "Client Deleted";
            }
        }
        public static string AddToCart(int ClientId, int ProductId) // added by Harby at 12:00 PM
        {
            using (ShopyCtx db = new())
            {
                var cartId = db.Carts.Where(c => c.ClientId.Equals(ClientId)).FirstOrDefault();
                Product.Update(ProductId, ClientId, Product.Properities.ClientId);
                Product.Update(ProductId, cartId, Product.Properities.CartId);
                return "successful Transactions";
            }
        }
        public static string RemoveFromCart(int ProductId)
        {
            using (ShopyCtx db = new())
            {
                Product.Update(ProductId, 3, Product.Properities.ClientId);
                Product.Update(ProductId, 1, Product.Properities.CartId);
                return "Item is remove from cart";
            }
        }
        public static string CheckOut(int clientId, int productId)
        {
            using (ShopyCtx db = new())
            {
                Product.Update(productId, 1, Product.Properities.CartId);
                return "successful Transactions";
            }
        }
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
