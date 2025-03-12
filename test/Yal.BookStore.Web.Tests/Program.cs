using Microsoft.AspNetCore.Builder;
using Yal.BookStore;
using Volo.Abp.AspNetCore.TestBase;

var builder = WebApplication.CreateBuilder();

builder.Environment.ContentRootPath = GetWebProjectContentRootPathHelper.Get("Yal.BookStore.HttpApi.Host.csproj");
await builder.RunAbpModuleAsync<BookStoreWebTestModule>(applicationName: "Yal.BookStore.HttpApi.Host" );

public partial class Program
{
}
