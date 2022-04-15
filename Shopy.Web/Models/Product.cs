using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Shopy.Web.Shared;
using Shopy.Web.Interfaces;

#nullable disable

namespace Shopy.Web.Models;

[Table("products")]
[Index(nameof(CartId), Name = "cartId")]
[Index(nameof(ClientUsername), Name = "clientId")]
public partial class Product : IProduct
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

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


    public enum Properities { ClientUsername, Category, Model, Price, Details, ImagePath, CartId };

    public string UpdateCartId(int id, int? value)
    {

        using (ShopyCtx db = new())
        {
            var product = db.Products.FirstOrDefault(p => p.Id == id);

            product.CartId = value;
            db.SaveChanges();
            return "Updated";
        }
    }
    public string UpdateClientUsername(int id, string value)
    {
        using (ShopyCtx db = new())
        {
            var product = db.Products.FirstOrDefault(p => p.Id == id);

            product.ClientUsername = value;
            db.SaveChanges();
            return "Updated";
        }
    }
    public Vendor GetVendor(int id)
    {
        using (ShopyCtx db = new())
        {
            var product = db.Products.Include(p => p.ModelNavigation).FirstOrDefault(p => p.Id == id);
            var Model = db.Models.Include(m => m.VendorNavigation).FirstOrDefault(m => m.Name == product.ModelNavigation.Name);
            return Model.VendorNavigation;
        }
    }

    public Product Get(int productId)
    {
        using (ShopyCtx db = new())
        {
            var product = db.Products.Include(p => p.ModelNavigation).FirstOrDefault(p => p.Id == productId);
            if (product == null) return new Product();
            return product;

        }

    }
    public bool Exist(int id)
    {
        using (ShopyCtx db = new())
        {
            Product product = db.Products.FirstOrDefault(p => p.Id == id);
            return product != null;
        }
    }
    public string Add(Product product)
    {
        using (ShopyCtx db = new())
        {
            var getProduct = db.Products.FirstOrDefault(v => v.Equals(product));
            if (getProduct != null)
            {
                return "Product already exists";
            }
            db.Products.Add(product);
            db.SaveChanges();
            return "Done adding Product";
        }
    }
    public string AddQuantity(Product product, int quanitity)
    {
        Product Product = new();
        using (ShopyCtx db = new())
        {
            for (int i = 0; i < quanitity; i++)
            {
                db.Products.Add(product);
                db.SaveChanges();
                product.Id++;
            }

        }
        return "Done adding Product Quantity";

    }

    public List<Product> AvailableProducts(int limit)
    {
        using (ShopyCtx db = new())
        {
            return db.Products.Where(p => p.ClientUsername == null).Take(limit).ToList();
        }
    }

    public string UpdateRate(int productId, decimal rate)
    {
        Model model = new();
        using (ShopyCtx db = new())
        {
            var product = db.Products.FirstOrDefault(p => p.Id == productId);
            product.Rate = rate;
            db.SaveChanges();
            model.EvaluateRate(product.Model);
        }
        return "updated Successfully";
    }

    public string UpdateModel(int productId, string model)
    {
        using (ShopyCtx db = new())
        {
            var product = db.Products.FirstOrDefault(p => p.Id == productId);
            product.Model = model;
            db.SaveChanges();
        }
        return "updated Successfully";

    }
}

