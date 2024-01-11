using DevNAS.Blogging.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace DevNAS.Blogging.Permissions;

public class BloggingPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var bloggingGroup = context.AddGroup(BloggingPermissions.GroupName, L("Permission:Posts"));
        //Define your own permissions here. Example:
        //myGroup.AddPermission(BloggingPermissions.MyPermission1, L("Permission:MyPermission1"));

        var booksPermission = bloggingGroup.AddPermission(BloggingPermissions.Posts.Default, L("Permission:Posts"));
        booksPermission.AddChild(BloggingPermissions.Posts.Create, L("Permission:Posts.Create"));
        booksPermission.AddChild(BloggingPermissions.Posts.Edit, L("Permission:Posts.Edit"));
        booksPermission.AddChild(BloggingPermissions.Posts.Delete, L("Permission:Posts.Delete"));


        var authorsPermission = bloggingGroup.AddPermission(BloggingPermissions.Authors.Default, L("Permission:Authors"));
        authorsPermission.AddChild(BloggingPermissions.Authors.Create, L("Permission:Authors.Create"));
        authorsPermission.AddChild(BloggingPermissions.Authors.Edit, L("Permission:Authors.Edit"));
        authorsPermission.AddChild(BloggingPermissions.Authors.Delete, L("Permission:Authors.Delete"));

    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<BloggingResource>(name);
    }
}
