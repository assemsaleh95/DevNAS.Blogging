using Volo.Abp.Modularity;

namespace DevNAS.Blogging;

public abstract class BloggingApplicationTestBase<TStartupModule> : BloggingTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
