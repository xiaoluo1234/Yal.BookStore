using Microsoft.AspNetCore.Builder;
using Yal.BookStore;
using Volo.Abp.AspNetCore.TestBase;

var builder = WebApplication.CreateBuilder();

builder.Environment.ContentRootPath = GetWebProjectContentRootPathHelper.Get("Yal.BookStore.Web.csproj");
await builder.RunAbpModuleAsync<BookStoreWebTestModule>(applicationName: "Yal.BookStore.Web" );

public partial class Program
{
}
