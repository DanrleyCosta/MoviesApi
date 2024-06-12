using FilmesApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

public class FilmeContextFactory : IDesignTimeDbContextFactory<FilmeContext>
{
    public FilmeContext CreateDbContext(string[] args)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<FilmeContext>();
        var connectionString = configuration.GetConnectionString("MovieConnection");

        optionsBuilder.UseSqlServer(connectionString);

        return new FilmeContext(optionsBuilder.Options);
    }
}
