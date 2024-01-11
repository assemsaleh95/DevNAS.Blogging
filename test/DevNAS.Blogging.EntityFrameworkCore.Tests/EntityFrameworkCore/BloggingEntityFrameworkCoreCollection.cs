using Xunit;

namespace DevNAS.Blogging.EntityFrameworkCore;

[CollectionDefinition(BloggingTestConsts.CollectionDefinitionName)]
public class BloggingEntityFrameworkCoreCollection : ICollectionFixture<BloggingEntityFrameworkCoreFixture>
{

}
