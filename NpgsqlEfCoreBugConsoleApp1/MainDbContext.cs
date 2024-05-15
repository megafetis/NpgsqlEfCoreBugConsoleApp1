using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NpgsqlEfCoreBugConsoleApp1
{
    public class MainDbContext:DbContext
    {
        public MainDbContext(DbContextOptions<MainDbContext> opts):base(opts)
        {
        }

        public DbSet<SomeInterestingArticle> Articles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SomeInterestingArticle>().Property(p => p.Tags).HasColumnType("text[]");
            base.OnModelCreating(modelBuilder);
        }
    }
}
