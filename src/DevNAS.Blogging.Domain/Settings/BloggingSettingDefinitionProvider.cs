using Volo.Abp.Settings;

namespace DevNAS.Blogging.Settings;

public class BloggingSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(BloggingSettings.MySetting1));
    }
}
