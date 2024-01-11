using Microsoft.AspNetCore.Builder;
using DevNAS.Blogging;
using Volo.Abp.AspNetCore.TestBase;

var builder = WebApplication.CreateBuilder();
await builder.RunAbpModuleAsync<BloggingWebTestModule>();

public partial class Program
{
}
