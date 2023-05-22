using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ueh.BackendApi.Data.EF
{
    public class UehDbContextFactory : IDesignTimeDbContextFactory<UehDbContext>
    {
        public UehDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("UehDb");

            var optionsBuilder = new DbContextOptionsBuilder<UehDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new UehDbContext(optionsBuilder.Options);
        }
    }
}
