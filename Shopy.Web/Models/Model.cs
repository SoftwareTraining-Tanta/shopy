using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Shopy.Models
{
    [Table("models")]
    public partial class Model
    {
        public Model()
        {
            Products = new HashSet<Product>();
        }

        [Key]
        [Column("name")]
        [StringLength(300)]
        public string Name { get; set; }
        [Required]
        [Column("imagePath")]
        [StringLength(512)]
        public string ImagePath { get; set; }
        [Column("price", TypeName = "decimal(9,2)")]
        public decimal Price { get; set; }
        [Column("salePrice", TypeName = "decimal(10,0)")]
        public decimal? SalePrice { get; set; }
        [Column("brand")]
        [StringLength(100)]
        public string Brand { get; set; }
        [Column("color")]
        [StringLength(50)]
        public string Color { get; set; }
        [Column("features")]
        [StringLength(600)]
        public string Features { get; set; }

        [InverseProperty(nameof(Product.ModelNavigation))]
        public virtual ICollection<Product> Products { get; set; }
    }
}
