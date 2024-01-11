using DevNAS.Blogging.Localization;
using DevNAS.Blogging.MultiTenancy;
using DevNAS.Blogging.Permissions;
using System.Threading.Tasks;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Identity.Web.Navigation;
using Volo.Abp.SettingManagement.Web.Navigation;
using Volo.Abp.TenantManagement.Web.Navigation;
using Volo.Abp.UI.Navigation;

namespace DevNAS.Blogging.Web.Menus;

public class BloggingMenuContributor : IMenuContributor
{
    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }
    }

    private Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        var administration = context.Menu.GetAdministration();
        var l = context.GetLocalizer<BloggingResource>();

        context.Menu.Items.Insert(
            0,
            new ApplicationMenuItem(
                BloggingMenus.Home,
                l["Menu:Home"],
                "~/",
                icon: "fas fa-home",
                order: 0
            )
        );
        context.Menu.AddItem(
            new ApplicationMenuItem(
                "Blogging",
                l["Menu:Blogging"],
                icon: "fa fa-solid fa-newspaper"
            ).AddItem(
                new ApplicationMenuItem(
                    "Blogging.Posts",
                    l["Menu:Posts"],
                    url: "/Posts"
                ).RequirePermissions(BloggingPermissions.Posts.Default) // Check the permission!
        ).AddItem(
                    new ApplicationMenuItem(
                        "Blogging.Posts",
                        l["Menu:PostList"],
                        url: "/Posts/PostList"
                        )
        ).AddItem( // ADDED THE NEW "AUTHORS" MENU ITEM UNDER THE "BOOK STORE" MENU
        new ApplicationMenuItem(
            "BooksStore.Authors",
            l["Menu:Authors"],
            url: "/Authors"
        ).RequirePermissions(BloggingPermissions.Authors.Default)
    )
);


        if (MultiTenancyConsts.IsEnabled)
        {
            administration.SetSubItemOrder(TenantManagementMenuNames.GroupName, 1);
        }
        else
        {
            administration.TryRemoveMenuItem(TenantManagementMenuNames.GroupName);
        }

        administration.SetSubItemOrder(IdentityMenuNames.GroupName, 2);
        administration.SetSubItemOrder(SettingManagementMenuNames.GroupName, 3);

        return Task.CompletedTask;
    }
}
