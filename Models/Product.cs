using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using System.Linq;

#nullable disable

namespace Models
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
        public int ClientId { get; set; }
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
        public int? CartId { get; set; }

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
            using (Shopy db = new())
            {
                Product product = db.Products.Where(p => p.Id == id).FirstOrDefault();
                switch (properities)
                {
                    case Properities.ClientId:
                        product.ClientId = value;
                        break;
                    case Properities.Category:
                        product.Category = value;
                        break;
                    case Properities.Model:
                        product.Model = value;
                        break;
                    case Properities.Price:
                        product.Price = value;
                        break;
                    case Properities.Details:
                        product.Details = value;
                        break;
                    case Properities.ImagePath:
                        product.ImagePath = value;
                        break;
                    case Properities.CartId:
                        product.CartId = value;
                        break;
                }
                db.SaveChanges();
                return "Updated";
            }
        }

        private static bool Exist(int id)
        {
            using (Shopy db = new())
            {
                Product product = db.Products.Where(p => p.Id == id).FirstOrDefault();
                return product != null;
            }
        }

    }

}
