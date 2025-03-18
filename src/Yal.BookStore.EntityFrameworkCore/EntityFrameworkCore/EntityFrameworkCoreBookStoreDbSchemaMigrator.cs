using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Yal.BookStore.Data;
using Volo.Abp.DependencyInjection;

namespace Yal.BookStore.EntityFrameworkCore;

public class EntityFrameworkCoreBookStoreDbSchemaMigrator
    : IBookStoreDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreBookStoreDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        await _serviceProvider
            .GetRequiredService<BookStoreDbContext>()
            .Database
            .MigrateAsync();
    }
}
