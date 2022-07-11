using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppAPI_FM.Models
{
    public class FavoriteMasterContext : DbContext

    {

        public DbSet<User> Users { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Order> Orders { get; set; }
        public FavoriteMasterContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //string StringConect = @"Data Source=.\SQLEXPRESS;Integrated Security=True;";

              optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=FavoriteMaster;Trusted_Connection=True;");
           // optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=FavoriteMaster;Trusted_Connection=True;");
        }
    }
}
