using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace AssetManagement.Database.data
{
    public class AssetManagementDbContextFactory : IDesignTimeDbContextFactory<AssetManagementDbContext>
    {
        public AssetManagementDbContext CreateDbContext(string[] args)
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var parantDirectory = (Directory.GetParent(currentDirectory)?.FullName) ?? throw new InvalidOperationException("The parent directory could not be determined.");
            var basePath = Path.Combine(parantDirectory, "AssetManagement.WebApi");

            // Build configuration
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.json")
            .Build();

            //Get the connection string
            var connectionString = configuration.GetConnectionString("LocalDatabase");

            //Set up DbContextOptions
            var _optionsBuilder = new DbContextOptionsBuilder<AssetManagementDbContext>();
            _optionsBuilder.UseSqlServer(connectionString);

            return new AssetManagementDbContext(_optionsBuilder.Options);
        }

    }
}