using Discount.Grpc.Models;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Data
{
    public class DiscountContext : DbContext
    {
        public DbSet<Coupon> Coupons { get; set; } = default!;

        public DiscountContext(DbContextOptions<DiscountContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Coupon>().HasData(
                new Coupon { Amount=1,Description="Iphone Discount", Id= 1, ProductName="Iphone 15"},
                new Coupon { Amount=1,Description="Samsung Discount", Id= 2, ProductName="Galaxy S24"}
            );
        }

    }
}
