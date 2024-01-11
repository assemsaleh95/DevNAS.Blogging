using System.Threading.Tasks;

namespace DevNAS.Blogging.Data;

public interface IBloggingDbSchemaMigrator
{
    Task MigrateAsync();
}
