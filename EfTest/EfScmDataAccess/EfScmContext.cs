using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace EfScmDataAccess
{
    // inherit DbContext
    public class EfScmContext : DbContext
    {
        // DbSet is a collection provided by EF
        public DbSet<PartType> Parts { get; set; }
        public DbSet<InventoryItem> Inventory { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Using SQLite with a file-based database
            optionsBuilder.UseSqlite("Filename=efscm.db");
        }
    }
}