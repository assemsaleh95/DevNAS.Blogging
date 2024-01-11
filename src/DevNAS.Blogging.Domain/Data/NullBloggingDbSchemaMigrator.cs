using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace DevNAS.Blogging.Data;

/* This is used if database provider does't define
 * IBloggingDbSchemaMigrator implementation.
 */
public class NullBloggingDbSchemaMigrator : IBloggingDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
