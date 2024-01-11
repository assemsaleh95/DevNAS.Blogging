using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace DevNAS.Blogging.Web;

[Dependency(ReplaceServices = true)]
public class BloggingBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "Blogging";
}
