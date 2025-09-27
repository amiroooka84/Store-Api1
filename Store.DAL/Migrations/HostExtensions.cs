using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using StoreApi.DAL.DB;
using System;

namespace StoreApi.DAL.Migrations
{
    public static class HostExtensions
    {
        //public static IHost MigrateDatabase<TContext>(this IHost host,
        //    Action<TContext, IServiceProvider> seeder,
        //    int? retry = 0) where TContext : DbContext
        //{
        //    int retryForAvailability = retry.Value;

        //    using (var scope = host.Services.CreateScope())
        //    {
        //        var services = scope.ServiceProvider;
        //        var logger = services.GetRequiredService<ILogger<TContext>>();
        //        var context = services.GetService<TContext>();
        //        //Same as the question
        //        context.Database.Migrate();
        //    }
        //    return host;
        //}
    }
}
