using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Shopy.Web.Models
{
    public partial class ShopyCtx : DbContext
    {
        public ShopyCtx()
        {
        }

        public ShopyCtx(DbContextOptions<ShopyCtx> options)
            : base(options)
        {
        }

        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Model> Models { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Vendor> Vendors { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // #warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server = localhost; database = shopy; username = root; password =2510203121;",
new MySqlServerVersion(new Version(10, 4, 17)));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cart>(entity =>
            {
                entity.HasOne(d => d.ClientNavigation)
                    .WithMany(p => p.Carts)
                    .HasForeignKey(d => d.ClientUsername)
                    .HasConstraintName("carts_ibfk_1");
            });

            modelBuilder.Entity<Client>(entity =>
            {
                entity.HasKey(e => e.Username)
                    .HasName("PRIMARY");
            });

            modelBuilder.Entity<Model>(entity =>
            {
                entity.HasKey(e => e.Name)
                    .HasName("PRIMARY");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.Rate).HasDefaultValueSql("'0.0'");

                entity.HasOne(d => d.Cart)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CartId)
                    .HasConstraintName("products_ibfk_3");

                entity.HasOne(d => d.ClientNavigation)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.ClientUsername)
                    .HasConstraintName("products_ibfk_2");

                entity.HasOne(d => d.ModelNavigation)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.Model)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("products_ibfk_4");

                entity.HasOne(d => d.VendorNavigation)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.VendorUsername)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("products_ibfk_1");
            });

            modelBuilder.Entity<Vendor>(entity =>
            {
                entity.HasKey(e => e.Username)
                    .HasName("PRIMARY");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
