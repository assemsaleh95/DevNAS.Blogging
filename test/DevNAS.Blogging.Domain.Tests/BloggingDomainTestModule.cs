using Volo.Abp.Modularity;

namespace DevNAS.Blogging;

[DependsOn(
    typeof(BloggingDomainModule),
    typeof(BloggingTestBaseModule)
)]
public class BloggingDomainTestModule : AbpModule
{

}
