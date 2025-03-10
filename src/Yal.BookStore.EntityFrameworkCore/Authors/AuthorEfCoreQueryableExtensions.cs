using System.Linq;

namespace Yal.BookStore.Authors;
public static class AuthorEfCoreQueryableExtensions
{
    public static IQueryable<Author> IncludeDetails(this IQueryable<Author> queryable, bool include = true)
    {
        if (!include)
        {
            return queryable;
        }

        return queryable
            ;
    }
}
