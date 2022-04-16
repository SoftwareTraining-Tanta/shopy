using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Shopy.Web.Interfaces;
using Shopy.Web.Shared;
#nullable disable

namespace Shopy.Web.Models
{
    [Table("vendors")]
    public partial class Vendor : IVendor
    {
        public Vendor()
        {
            Models = new HashSet<Model>();
        }

        [Key]
        [Column("username")]
        [StringLength(30)]
        public string Username { get; set; }
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
        [Required]
        [Column("verificationCode")]
        [StringLength(6)]
        public string VerificationCode { get; set; }
        [Required]
        [Column("isVerified")]
        public bool IsVerified { get; set; }

        [InverseProperty(nameof(Model.VendorNavigation))]
        public virtual ICollection<Model> Models { get; set; }

        public string Add(Vendor vendor)
        {
            using (ShopyCtx db = new())
            {
                var getVendor = db.Vendors.FirstOrDefault(cl => cl.Equals(vendor));
                if (getVendor != null)
                {
                    return "Vendor already exists";
                }
                vendor.Password = vendor.Password.ToSha256();
                vendor.VerificationCode = "null1";
                db.Vendors.Add(vendor);
                db.SaveChanges();
            }
            return "Vendor Added Successfully";
        }

        public List<Vendor> AllVendors(int limit)
        {
            using (ShopyCtx db = new())
            {
                return db.Vendors.Take(limit).ToList();
            }
        }

        public string Delete(string username)
        {
            var isFound = Exist(username);
            if (!isFound)
                return MyExceptions.VendorNotFound(username);
            using (ShopyCtx db = new())
            {
                Vendor vendor = db.Vendors.FirstOrDefault(v => v.Username == username);
                db.Vendors.Remove(vendor);
                db.SaveChanges();
            }
            return "Vendor Deleted Successfully";
        }

        public bool Exist(string username)
        {
            using (ShopyCtx db = new())
            {
                Vendor vendor = db.Vendors.FirstOrDefault(v => v.Username == username);
                return vendor != null;
            }
        }

        public Vendor Get(string username)
        {
            using (ShopyCtx db = new())
            {
                Vendor vendor = db.Vendors.FirstOrDefault(v => v.Username == username);
                return vendor;
            }
        }

        public string UpdatePassword(string username, string newPassword)
        {
            var isFound = Exist(username);
            if (!isFound)
                return MyExceptions.VendorNotFound(username);
            using (ShopyCtx db = new())
            {
                Vendor vendor = db.Vendors.FirstOrDefault(v => v.Username == username);
                vendor.Password = newPassword.ToSha256();
                db.SaveChanges();
            }
            return "password is  updated successfully";
        }

        public string UpdateVerificationCode(string vendorUsername, string verificationCode)
        {

            using (ShopyCtx db = new())
            {
                Vendor vendor = db.Vendors.FirstOrDefault(v => v.Username == vendorUsername);
                vendor.VerificationCode = verificationCode;
                db.SaveChanges();
            }
            return "Verification code is updated successfully";

        }

        public List<Model> VendorModels(string Username)
        {
            using (ShopyCtx db = new())
            {
                var models = db.Models
                .Include(m => m.VendorNavigation)
                .Where(m => m.VendorNavigation.Username == Username)
                .ToList();

                if (models != null) return models;
                return new List<Model>();
            }
        }
        public Dictionary<string, int> ProductCountPerModel(string username)
        {
            Model model = new();
            Vendor vendor = new();
            Vendor vendor1 = vendor.Get(username);
            Dictionary<string, int> dict = new Dictionary<string, int>();
            List<Model> models = VendorModels(vendor1.Username);
            foreach (Model model1 in models)
            {
                dict[model1.Name] = model1.AvailableProducts(model1.Name);
            }
            return dict;



            //     return db.Models
            //     .Include(m => m.VendorNavigation)
            //     .Where(m => m.VendorNavigation.Username == Username)
            //     .GroupBy(m => m.Name)
            //     .ToDictionary(m => m.Key, m => m.Count());
            // }

        }
    }
}
