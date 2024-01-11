using DevNAS.Blogging.Samples;
using Xunit;

namespace DevNAS.Blogging.EntityFrameworkCore.Applications;

[Collection(BloggingTestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<BloggingEntityFrameworkCoreTestModule>
{

}
