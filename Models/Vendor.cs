using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using System.Linq;
#nullable disable

namespace Shopy.Models
{
    [Table("vendors")]
    public partial class Vendor
    {
        public Vendor()
        {
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

        [InverseProperty(nameof(Product.Vendor))]
        public virtual ICollection<Product> Products { get; set; }

        public static string Add(Vendor vendor)
        {
            using (ShopyCtx db = new())
            {
                var getVendor = db.Vendors.Where(v => v.Equals(vendor)).FirstOrDefault();
                if (getVendor != null)
                {
                    return "Client already exists";
                }
                db.Vendors.Add(vendor);
                db.SaveChanges();
                return "Done adding client";
            }
        }
    }
}
