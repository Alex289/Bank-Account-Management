using BankAccountManagementApi.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Net.Http;

namespace BankAccountManagementApi.IntegrationsTests
{
    public class DefaultFactory
    {
        protected readonly HttpClient TestClient;
        protected DefaultFactory()
        {
            var appFactory = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<BankContext>));
                        services.Remove(descriptor);

                        services.AddDbContext<BankContext>(options => { options.UseInMemoryDatabase("BankAccountManagement"); });

                        var serviceProvider = services.BuildServiceProvider();

                        using var scope = serviceProvider.CreateScope();
                        var db = scope.ServiceProvider.GetRequiredService<BankContext>();

                        try
                        {
                            DbInitializer.Initialize(db);
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                    });
                });

            TestClient = appFactory.CreateClient();
        }
    }
}
