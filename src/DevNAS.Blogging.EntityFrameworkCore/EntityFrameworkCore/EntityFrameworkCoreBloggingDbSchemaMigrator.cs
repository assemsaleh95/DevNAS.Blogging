using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using DevNAS.Blogging.Data;
using Volo.Abp.DependencyInjection;

namespace DevNAS.Blogging.EntityFrameworkCore;

public class EntityFrameworkCoreBloggingDbSchemaMigrator
    : IBloggingDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreBloggingDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolve the BloggingDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<BloggingDbContext>()
            .Database
            .MigrateAsync();
    }
}
