using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Busines> Businesses { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Workload> Workloads { get; set; }
        public ApplicationDbContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=businesses;Username=postgres;Password=root");
        }
    }
}
