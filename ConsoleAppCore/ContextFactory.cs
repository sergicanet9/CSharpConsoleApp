using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;

public class ContextFactory : IDesignTimeDbContextFactory<Model>
{
    private static IConfiguration configuration;
    
    public Model CreateDbContext(string[] args)
    {
        configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();

        var optionsBuilder = new DbContextOptionsBuilder<Model>();
        optionsBuilder.UseSqlite(configuration.GetConnectionString("DB1"));

        return new Model(optionsBuilder.Options);
    }
}