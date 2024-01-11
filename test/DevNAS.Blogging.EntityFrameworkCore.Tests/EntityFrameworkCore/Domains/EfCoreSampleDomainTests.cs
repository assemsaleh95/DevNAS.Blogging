using DevNAS.Blogging.Samples;
using Xunit;

namespace DevNAS.Blogging.EntityFrameworkCore.Domains;

[Collection(BloggingTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<BloggingEntityFrameworkCoreTestModule>
{

}
