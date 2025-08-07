using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyWebApi.Models; 

namespace MyWebApi.Data
{
    public class ApplicationDBContext : IdentityDbContext<AppUser>
    {
        //run : dotnet ef migrations add init : creates a migration folder with all the models converted into table & sql scripts ready.
        //run: dotnet ef database update : to convert your models in database tables via ApplicationDBContext       
        public ApplicationDBContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Portfolio> Portfolios{ get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Portfolio>(x => x.HasKey(p => new { p.AppUserId, p.StockID }));
            builder.Entity<Portfolio>()
                .HasOne(u => u.AppUser)
                .WithMany(u => u.Portfolios)
                .HasForeignKey(p => p.AppUserId);
            builder.Entity<Portfolio>()
                .HasOne(u => u.stock)
                .WithMany(u => u.Portfolios)
                .HasForeignKey(p => p.StockID);
            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole{
                    Name="Admin",
                    NormalizedName="ADMIN"
                },
                new IdentityRole{
                    Name="User",
                    NormalizedName="USER"
                },
            };
            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}