using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using petBack.Models;

namespace petBack.Context
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        // Construtor
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }

        //mapeamento
        public DbSet<Product> Products { get; set; }

        //fluent api
        protected override void OnModelCreating(ModelBuilder mb)
        {
            base.OnModelCreating(mb);
            mb.Entity<Product>().HasKey(p => p.ProductId);

            mb.Entity<Product>()
                .Property(p => p.Title).HasMaxLength(200).IsRequired();

            mb.Entity<Product>()
               .Property(p => p.Description).HasMaxLength(500).IsRequired();

            mb.Entity<Product>()
               .Property(p => p.ImagemUrl).HasMaxLength(500).IsRequired();

            mb.Entity<Product>()
               .Property(p => p.Price)
               .HasColumnType("decimal(18,2)") // Define o tipo decimal com precisão e escala
               .IsRequired();
        }
        

    }
}