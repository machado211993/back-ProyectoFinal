using Microsoft.EntityFrameworkCore;
using ProductCategoryCrud.Models;

namespace ProductCategoryCrud.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }

        // Agregar las entidades de venta
        public DbSet<Venta> Ventas { get; set; }
        public DbSet<VentaProducto> VentaProductos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seed roles
            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "Admin" },
                new Role { Id = 2, Name = "User" }
            );

            // Configuración de las relaciones entre Venta y VentaProducto
            modelBuilder.Entity<VentaProducto>()
                .HasOne(vp => vp.Venta)
                .WithMany(v => v.VentaProductos)
                .HasForeignKey(vp => vp.VentaId)
                .OnDelete(DeleteBehavior.Cascade);  // Si una venta se elimina, los productos asociados también se eliminarán

            modelBuilder.Entity<VentaProducto>()
                .HasOne(vp => vp.Product)
                .WithMany() // Si un producto puede estar en muchas ventas, no necesitamos configurar una propiedad inversa en Product
                .HasForeignKey(vp => vp.ProductId)
                .OnDelete(DeleteBehavior.Restrict); // No permitir que se elimine un producto si está relacionado con una venta

            // Configuración de la relación entre Venta y User
            modelBuilder.Entity<Venta>()
                .HasOne(v => v.User)
                .WithMany()  // Si un usuario puede hacer muchas ventas
                .HasForeignKey(v => v.UserId)
                .OnDelete(DeleteBehavior.Restrict);  // No eliminar ventas si se elimina un usuario

            base.OnModelCreating(modelBuilder);
        }
    }
}
