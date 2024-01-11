using DevNAS.Blogging.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace DevNAS.Blogging.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class BloggingController : AbpControllerBase
{
    protected BloggingController()
    {
        LocalizationResource = typeof(BloggingResource);
    }
}
