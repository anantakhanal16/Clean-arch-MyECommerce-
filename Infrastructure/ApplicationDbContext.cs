using Core.Entites;
using Core.Entites.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public  class ApplicationDbContext:  IdentityDbContext<AppUser>
    {
       
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<AppUser> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductType> ProductType { get; set; }
        public DbSet<ProductBrand> ProductBrand { get; set; }
        public DbSet<Cart> Cart { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderItem> Orderitems { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasOne(p => p.ProductBrand)
                .WithMany()
                .HasForeignKey(p => p.ProductBrandId);

            modelBuilder.Entity<Product>()
                .HasOne(p => p.ProductType)
                .WithMany()
                .HasForeignKey(p => p.ProductTypeId);

                modelBuilder.Entity<Cart>()
                .HasOne(c => c.Product)
                .WithMany()
                .HasForeignKey(c => c.ProductId)
                .OnDelete(DeleteBehavior.NoAction); 

            modelBuilder.Entity<Cart>()
                .HasOne(c => c.ProductBrand)
                .WithMany()
                .HasForeignKey(c => c.ProductBrandId);

            modelBuilder.Entity<Cart>()
                .HasOne(c => c.ProductType)
                .WithMany()
                .HasForeignKey(c => c.ProductTypeId);


            modelBuilder.Entity<Order>()
             .HasMany(order => order.OrderItems)
             .WithOne(orderDetail => orderDetail.Order)
             .HasForeignKey(orderDetail => orderDetail.OrderId);

            modelBuilder.Entity<OrderItem>()
               .HasOne(orderDetail => orderDetail.Product)
               .WithMany()  
               .HasForeignKey(orderDetail => orderDetail.ProductId);

            base.OnModelCreating(modelBuilder);
        }
        
    }
}
