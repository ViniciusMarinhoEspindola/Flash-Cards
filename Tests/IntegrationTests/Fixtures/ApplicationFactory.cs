using Infraestructure.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FlashCards.IntegrationTests.Fixtures;

public class ApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing");

        builder.ConfigureServices(services =>
        {
            // Remove all EF Core registrations for DBContext (including internal IDbContextOptionsConfiguration<DBContext>)
            var toRemove = services
                .Where(d =>
                    d.ServiceType == typeof(DBContext) ||
                    d.ServiceType == typeof(DbContextOptions<DBContext>) ||
                    (d.ServiceType.IsGenericType &&
                     d.ServiceType.GetGenericTypeDefinition().Name.StartsWith("IDbContextOptions") &&
                     d.ServiceType.GenericTypeArguments.Length == 1 &&
                     d.ServiceType.GenericTypeArguments[0] == typeof(DBContext)))
                .ToList();

            foreach (var d in toRemove)
                services.Remove(d);

            var dbName = $"TestDB_{Guid.NewGuid()}";
            services.AddDbContext<DBContext>(options =>
                options.UseInMemoryDatabase(dbName));
        });
    }
}
