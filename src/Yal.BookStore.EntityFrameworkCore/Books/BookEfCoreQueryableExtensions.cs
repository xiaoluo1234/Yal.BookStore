using System.Linq;

namespace Yal.BookStore.Books;
public static class BookEfCoreQueryableExtensions
{
    public static IQueryable<Book> IncludeDetails(this IQueryable<Book> queryable, bool include = true)
    {
        if (!include)
        {
            return queryable;
        }

        return queryable
            ;
    }
}
