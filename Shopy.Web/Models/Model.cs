using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Shopy.Web.Interfaces;

#nullable disable

namespace Shopy.Web.Models
{
    [Table("models")]
    public partial class Model : IModel
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
        [Column("rate", TypeName = "decimal(2,1)")]
        public decimal? Rate { get; set; }
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

        public void AddProducts(int modelName, int qt)
        {
            throw new NotImplementedException();
        }

        public int CntProducts(int modelName)
        {
            throw new NotImplementedException();
        }

        public float GetRate(int modelName)
        {
            throw new NotImplementedException();
        }

        public void UpdateBrand(int modelName, string value)
        {
            throw new NotImplementedException();
        }

        public void UpdateColor(int modelName, string value)
        {
            throw new NotImplementedException();
        }

        public void UpdateFeatures(int modelName, string value)
        {
            throw new NotImplementedException();
        }

        public void UpdateImagePath(int modelName, string value)
        {
            throw new NotImplementedException();
        }

        public void UpdateName(int modelName, string value)
        {
            throw new NotImplementedException();
        }

        public void UpdatePrice(int modelName, decimal value)
        {
            throw new NotImplementedException();
        }

        public void UpdateSalePrice(int modelName, decimal value)
        {
            throw new NotImplementedException();
        }
    }
}
