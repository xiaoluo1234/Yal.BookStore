using System.Threading.Tasks;

namespace Yal.BookStore.Data;

public interface IBookStoreDbSchemaMigrator
{
    Task MigrateAsync();
}
