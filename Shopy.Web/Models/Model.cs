using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Shopy.Web.Dtos;
using Shopy.Web.Interfaces;
using Shopy.Web.Shared;

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
        [Column("vendorUsername")]
        [StringLength(30)]
        public string VendorUsername { get; set; }
        [Column("salePrice", TypeName = "decimal(10,0)")]

        public decimal? SalePrice { get; set; }
        [Column("rate", TypeName = "decimal(2,1)")]
        public decimal? Rate { get; set; }
        [Column("brand")]
        [StringLength(100)]
        public string Brand { get; set; }
        [Required]
        [Column("category")]
        [StringLength(20)]
        public string Category { get; set; }
        [Column("color")]
        [StringLength(50)]
        public string Color { get; set; }
        [Column("features")]
        [StringLength(600)]
        public string Features { get; set; }
        public bool IsSale { get; set; }

        [InverseProperty(nameof(Product.ModelNavigation))]
        public virtual ICollection<Product> Products { get; set; }
        [ForeignKey(nameof(VendorUsername))]
        [InverseProperty(nameof(Vendor.Models))]
        public virtual Vendor VendorNavigation { get; set; }


        public void AddModelWithProducts(Model model, int qt)
        {
            Product Product = new Product();
            using (ShopyCtx db = new())
            {
                db.Models.Add(model);
                db.SaveChanges();
            }
            Product product = new();
            product.Model = model.Name;
            product.AddQuantity(product, qt);
        }



        public int CntProducts(string modelName)
        {
            using (ShopyCtx db = new())
            {
                int count = db.Products.Where(p => p.Model == modelName).Count();
                return count;
            }
        }

        public string EvaluateRate(string modelName)
        {
            using (ShopyCtx db = new())
            {
                List<Product> ratedProducts = db.Products.Where(p => (p.Model == modelName && p.Rate != null)).ToList();
                decimal? sum = 0;
                foreach (Product p in ratedProducts)
                {
                    sum += p.Rate;
                }
                decimal? avg = sum / ratedProducts.Count;
                Model model = db.Models.FirstOrDefault(m => m.Name == modelName);
                model.Rate = avg;
                db.SaveChanges();
            }
            return "Evaluation Done";
        }

        public Model Get(string name)
        {
            using (ShopyCtx db = new())
            {
                return db.Models.FirstOrDefault(m => m.Name == name);
            }
        }

        public void UpdateBrand(string modelName, string value)
        {
            using (ShopyCtx db = new())
            {
                Model model = db.Models.FirstOrDefault(m => m.Name == modelName);
                model.Brand = value;
                db.SaveChanges();
            }
        }

        public int AvailableProducts(string modelName)
        {
            int count = 0;
            using (ShopyCtx db = new())
            {
                Model model = db.Models.Include(m => m.Products).FirstOrDefault(m => m.Name == modelName);
                count = model.Products.Where(p => p.ClientUsername == null).Count();
                return count;
            }
        }
        public List<ProductDto> GetProducts(string modelName)
        {

            using (ShopyCtx db = new())
            {
                Model model = db.Models
                .Include(m => m.Products)
                .FirstOrDefault(m => m.Name == modelName);
                return model.Products.Where(p => p.ClientUsername == null).ToList().AsDto();
            }
        }
        public List<Model> GetAllOrderedbySale(int limit)
        {
            using (ShopyCtx db = new())
            {
                List<Model> models = db.Models
                .OrderByDescending(m => m.IsSale)
                .ThenByDescending(m => m.Rate)
                .Take(limit).ToList();
                return models;
            }
        }
        public List<Model> GetAllOrderedbyRate(int limit)
        {
            using (ShopyCtx db = new())
            {
                List<Model> models = db.Models
                .OrderByDescending(m => m.Rate)
                .ThenByDescending(m => m.IsSale)
                .Take(limit).ToList();
                return models;
            }
        }
        public void DeleteModel(string modelName)
        {
            Model model = new();
            model = model.Get(modelName);
            using (ShopyCtx db = new())
            {
                db.Models.Remove(model);
                db.SaveChanges();
            }
        }
        public void UpdateColor(string modelName, string value)
        {
            using (ShopyCtx db = new())
            {
                Model model = db.Models.FirstOrDefault(m => m.Name == modelName);
                model.Color = value;
                db.SaveChanges();
            }
        }
        public void UpdateFeatures(string modelName, string value)
        {
            using (ShopyCtx db = new())
            {
                Model model = db.Models.FirstOrDefault(m => m.Name == modelName);
                model.Features = value;
                db.SaveChanges();
            }
        }
        public string UpdateImagePath(string modelName, string value)
        {
            using (ShopyCtx db = new())
            {
                Model model = db.Models.FirstOrDefault(m => m.Name == modelName);
                model.ImagePath = value;
                db.SaveChanges();
            }
            return value;
        }

        public void UpdateIsSale(string modelName, bool value)
        {
            using (ShopyCtx db = new())
            {
                Model model = db.Models.FirstOrDefault(m => m.Name == modelName);
                model.IsSale = value;
                db.SaveChanges();
            }
        }

        public void UpdateName(string modelName, string value)
        {
            using (ShopyCtx db = new())
            {
                Model model = db.Models.FirstOrDefault(m => m.Name == modelName);
                model.Name = value;
                db.SaveChanges();
            }
        }
        public void UpdatePrice(string modelName, decimal value)
        {
            using (ShopyCtx db = new())
            {
                Model model = db.Models.FirstOrDefault(m => m.Name == modelName);
                model.Price = value;
                db.SaveChanges();
            }
        }
        public void UpdateSalePrice(string modelName, decimal value)
        {
            using (ShopyCtx db = new())
            {
                Model model = db.Models.FirstOrDefault(m => m.Name == modelName);
                model.SalePrice = value;
                db.SaveChanges();
            }
        }
    }
}
