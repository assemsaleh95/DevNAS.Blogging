using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace DevNAS.Blogging.Pages;

public class Index_Tests : BloggingWebTestBase
{
    [Fact]
    public async Task Welcome_Page()
    {
        var response = await GetResponseAsStringAsync("/");
        response.ShouldNotBeNull();
    }
}
