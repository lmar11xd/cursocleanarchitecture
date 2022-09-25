using Microsoft.EntityFrameworkCore;
using NorthWind.Entities.POCOs;

namespace NorthWind.Repositories.EFCore.DataContext
{
    public class NorthWindContext : DbContext
    {
        public NorthWindContext (DbContextOptions<NorthWindContext> options) : base(options)
        {

        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        //Reglas de Validación
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Customer
            modelBuilder.Entity<Customer>()
                .Property(c => c.Id)
                .HasMaxLength(5)
                .IsFixedLength();
            modelBuilder.Entity<Customer>()
                .Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(40);

            //Product
            modelBuilder.Entity<Product>()
                .Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(40);

            //Order
            modelBuilder.Entity<Order>()
                .Property(o => o.CustomerId)
                .IsRequired()
                .HasMaxLength(5)
                .IsFixedLength();
            modelBuilder.Entity<Order>()
                .Property(o => o.ShipAddress)
                .IsRequired()
                .HasMaxLength(60);
            modelBuilder.Entity<Order>()
                .Property(o => o.ShipCity)
                .IsRequired()
                .HasMaxLength(15);
            modelBuilder.Entity<Order>()
                .Property(o => o.ShipCountry)
                .IsRequired()
                .HasMaxLength(15);
            modelBuilder.Entity<Order>()
                .Property(o => o.ShipPostalCode)
                .IsRequired()
                .HasMaxLength(10);

            //OrderDetail
            modelBuilder.Entity<OrderDetail>()
                .HasKey(od => new { od.OrderId, od.ProductId });

            //Relations
            modelBuilder.Entity<Order>()
                .HasOne<Customer>()
                .WithMany()
                .HasForeignKey(o => o.CustomerId);

            modelBuilder.Entity<OrderDetail>()
                .HasOne<Product>()
                .WithMany()
                .HasForeignKey(od => od.ProductId);

            //Data
            modelBuilder.Entity<Product>()
                .HasData(
                    new Product {Id = 1, Name = "Chai" },
                    new Product { Id = 2, Name = "Chang" },
                    new Product { Id = 3, Name = "Aniseed Syrup" }
                );
            modelBuilder.Entity<Customer>()
                .HasData(
                    new Customer { Id = "ALFKI", Name = "Alfreds F." },
                    new Customer { Id = "ANATR", Name = "Ana Trujillo" },
                    new Customer { Id = "ANTON", Name = "Antonio Moreno" }
                );
        }

    }
}
