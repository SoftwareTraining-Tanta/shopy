using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

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
    }
}
