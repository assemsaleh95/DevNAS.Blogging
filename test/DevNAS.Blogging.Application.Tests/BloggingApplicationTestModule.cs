using Volo.Abp.Modularity;

namespace DevNAS.Blogging;

[DependsOn(
    typeof(BloggingApplicationModule),
    typeof(BloggingDomainTestModule)
)]
public class BloggingApplicationTestModule : AbpModule
{

}
