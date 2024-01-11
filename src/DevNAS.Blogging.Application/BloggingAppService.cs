using System;
using System.Collections.Generic;
using System.Text;
using DevNAS.Blogging.Localization;
using Volo.Abp.Application.Services;

namespace DevNAS.Blogging;

/* Inherit your application services from this class.
 */
public abstract class BloggingAppService : ApplicationService
{
    protected BloggingAppService()
    {
        LocalizationResource = typeof(BloggingResource);
    }
}
