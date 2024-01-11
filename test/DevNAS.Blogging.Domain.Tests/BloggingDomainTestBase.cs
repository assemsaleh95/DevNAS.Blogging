using Volo.Abp.Modularity;

namespace DevNAS.Blogging;

/* Inherit from this class for your domain layer tests. */
public abstract class BloggingDomainTestBase<TStartupModule> : BloggingTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
