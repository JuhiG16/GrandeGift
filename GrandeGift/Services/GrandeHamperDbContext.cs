using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using GrandeGift.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace GrandeGift.Services
{
    public class GrandeHamperDbContext : IdentityDbContext<User, Role, int, IdentityUserClaim<int>, UserRole,
        IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public DbSet<Customer> tblCustomer { get; set; }
        public DbSet<Admin> tblAdmin { get; set; }
        public DbSet<Hamper> tblHamper { get; set; }
        public DbSet<Category> tblCategory { get; set; }
        public DbSet<Feedback> tblFeedback { get; set; }
        public DbSet<Address> tblAddress { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
    //            optionsBuilder.UseSqlServer(@"Data Source=NJ;Initial Catalog=GrandeHamperDb;Integrated Security=True");
            optionsBuilder.UseSqlServer("Data Source=NJ;database=GrandeHamperDb;Integrated Security=True");
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

    }
}
