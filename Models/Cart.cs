using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Models
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
        [Column("clientId")]
        public int ClientId { get; set; }
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
    }
}
