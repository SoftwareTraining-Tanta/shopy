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
    [Index(nameof(ClientId), Name = "clientId")]
    [Index(nameof(VendorId), Name = "vendorId")]
    public partial class Product
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("vendorId")]
        public int VendorId { get; set; }
        [Column("clientId")]
        public Nullable<int> ClientId { get; set; }
        [Required]
        [Column("category")]
        [StringLength(20)]
        public string Category { get; set; }
        [Column("model")]
        [StringLength(30)]
        public string Model { get; set; }
        [Column("price", TypeName = "decimal(7,2)")]
        public decimal Price { get; set; }
        [Column("details")]
        [StringLength(512)]
        public string Details { get; set; }
        [Column("imagePath")]
        [StringLength(512)]
        public string ImagePath { get; set; }
        [Column("cartId")]
        public Nullable<int> CartId { get; set; }
        [ForeignKey(nameof(CartId))]
        [InverseProperty("Products")]
        public virtual Cart Cart { get; set; }
        [ForeignKey(nameof(ClientId))]
        [InverseProperty("Products")]
        public virtual Client Client { get; set; }
        [ForeignKey(nameof(VendorId))]
        [InverseProperty("Products")]
        public virtual Vendor Vendor { get; set; }

        public enum Properities { ClientId, Category, Model, Price, Details, ImagePath, CartId };

        public static string Update(int id, dynamic value, Properities properities)
        {
            bool isFound = Exist(id);
            if (!isFound) return MyExceptions.ProductNotFound(id);
            using (ShopyCtx db = new())
            {
                var product = db.Products.FirstOrDefault(p => p.Id == id);
                switch (properities)
                {
                    case Properities.ClientId:
                        if (product != null) product.ClientId = value;
                        break;
                    case Properities.Category:
                        if (product != null) product.Category = value;
                        break;
                    case Properities.Model:
                        if (product != null) product.Model = value;
                        break;
                    case Properities.Price:
                        if (product != null) product.Price = value;
                        break;
                    case Properities.Details:
                        if (product != null) product.Details = value;
                        break;
                    case Properities.ImagePath:
                        if (product != null) product.ImagePath = value;
                        break;
                    case Properities.CartId:
                        if (product != null) product.CartId = value;
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
                return db.Products.Where(p => p.ClientId == null).ToList();
            }
        }
    }

}
