using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

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

        [InverseProperty(nameof(Cart.Client))]
        public virtual ICollection<Cart> Carts { get; set; }
        [InverseProperty(nameof(Product.Client))]
        public virtual ICollection<Product> Products { get; set; }
    }
}
