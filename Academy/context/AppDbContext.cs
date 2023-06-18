using Academy.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.context
{
    internal class AppDbContext:DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Group> Groups { get; set; } 

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connection = "Server=MSI\\SQLEXPRESS;Database=Academy;Trusted_Connection=True;TrustServerCertificate=True";
            optionsBuilder.UseSqlServer(connection);

        }
    }
}
