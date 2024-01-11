using DevNAS.Blogging.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace DevNAS.Blogging.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(BloggingEntityFrameworkCoreModule),
    typeof(BloggingApplicationContractsModule)
    )]
public class BloggingDbMigratorModule : AbpModule
{
}
