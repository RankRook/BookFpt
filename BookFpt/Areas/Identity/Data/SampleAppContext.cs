using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BookFpt.Areas.Identity.Data;
using BookFpt.Models;

namespace BookFpt.Data
{
    public class SampleAppContext : IdentityDbContext<SampleAppUser>
    {
        public SampleAppContext(DbContextOptions<SampleAppContext> options)
            : base(options)
        {
        }

        public DbSet<Order> Order { get; set; }
        public DbSet<Book> Book { get; set; }
        public DbSet<OrderDetail> OrderDetail { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<CategoryRequest> CategoryRequest { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
