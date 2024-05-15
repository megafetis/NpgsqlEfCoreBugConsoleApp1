using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using NpgsqlEfCoreBugConsoleApp1;

namespace EfStorage
{
    public class MainDbContextFactory : IDesignTimeDbContextFactory<MainDbContext>
    {
        public MainDbContext CreateDbContext(string[] args)
        {
            var o = new DbContextOptionsBuilder<MainDbContext>();
            o.EnableDetailedErrors()
                //.LogTo(Console.WriteLine)
                .UseNpgsql(Constants.ConnectionString);
            return new MainDbContext(o.Options);
        }
    }
}
