using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Shopy.Models.Shared;

#nullable disable

namespace Shopy.Models
{
    [Table("products")]
    [Index(nameof(CartId), Name = "cartId")]
    [Index(nameof(ClientUsername), Name = "clientId")]
    [Index(nameof(VendorUsername), Name = "vendorId")]
    public partial class Product
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("vendorUsername")]
        [StringLength(30)]
        public string VendorUsername { get; set; }
        [Required]
        [Column("category")]
        [StringLength(20)]
        public string Category { get; set; }
        [Required]
        [Column("model")]
        [StringLength(300)]
        public string Model { get; set; }
        [Column("clientUsername")]
        [StringLength(30)]
        public string ClientUsername { get; set; }
        [Column("cartId")]
        public int? CartId { get; set; }
        [Column("rate", TypeName = "decimal(2,1)")]
        public decimal? Rate { get; set; }

        [ForeignKey(nameof(CartId))]
        [InverseProperty("Products")]
        public virtual Cart Cart { get; set; }
        [ForeignKey(nameof(ClientUsername))]
        [InverseProperty(nameof(Client.Products))]
        public virtual Client ClientNavigation { get; set; }
        [ForeignKey(nameof(Model))]
        [InverseProperty("Products")]
        public virtual Model ModelNavigation { get; set; }
        [ForeignKey(nameof(VendorUsername))]
        [InverseProperty(nameof(Vendor.Products))]
        public virtual Vendor VendorNavigation { get; set; }

        public enum Properities { ClientUsername, Category, Model, Price, Details, ImagePath, CartId };

        public static string Update(int id, dynamic value, Properities properities)
        {
            bool isFound = Exist(id);
            if (!isFound) return MyExceptions.ProductNotFound(id);
            using (ShopyCtx db = new())
            {
                var product = db.Products.FirstOrDefault(p => p.Id == id);
                switch (properities)
                {
                    case Properities.ClientUsername:
                        if (product != null) product.ClientUsername = value;
                        break;
                    case Properities.Category:
                        if (product != null) product.Category = value;
                        break;
                    case Properities.Model:
                        if (product != null) product.Model = value;
                        break;
                }
                db.SaveChanges();
                return "Updated";
            }
        }

        public static dynamic Get(int productId)
        {
            bool isFound = Exist(productId);
            if (!isFound)
            {
                return MyExceptions.ProductNotFound(productId);
            }
            using (ShopyCtx db = new())
            {
                return db.Products.FirstOrDefault(p => p.Id == productId);
            }

        }
        private static bool Exist(int id)
        {
            using (ShopyCtx db = new())
            {
                Product product = db.Products.FirstOrDefault(p => p.Id == id);
                return product != null;
            }
        }
        public static string Add(Product product)
        {
            using (ShopyCtx db = new())
            {
                var getProduct = db.Products.FirstOrDefault(v => v.Equals(product));
                if (getProduct != null)
                {
                    return "Client already exists";
                }
                db.Products.Add(product);
                db.SaveChanges();
                return "Done adding client";
            }
        }

        public static List<Product> AvailableProducts()
        {
            using (ShopyCtx db = new())
            {
                return db.Products.Where(p => p.ClientUsername == null).ToList();
            }
        }
    }

}
