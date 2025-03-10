using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Yal.BookStore.Authors;
using Yal.BookStore.Books;

// [ConnectionStringName(EquipmentsDbProperties.ConnectionStringName)]
namespace Yal.BookStore.EntityFrameworkCore;
public interface IBookStoreDbContext : IEfCoreDbContext
{
    public DbSet<Book> Books { get; set; }

    public DbSet<Author> Authors { get; set; }
}