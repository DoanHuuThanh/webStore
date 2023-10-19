using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace doanhuuthanh_web.Data.EF
{
    public class doanhuuthanhDbContextFactory : IDesignTimeDbContextFactory<doanhuuthanhDbContext>
    {   
        public doanhuuthanhDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsetting.json")
                .Build();

            var connectionString = configuration.GetConnectionString("doanhuuthanh_webDb");

            var optionsBuilder = new DbContextOptionsBuilder<doanhuuthanhDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new doanhuuthanhDbContext(optionsBuilder.Options);
        }
    }
}
