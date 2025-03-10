using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Yal.BookStore.EntityFrameworkCore;

namespace Yal.BookStore.Books
{
    /// <summary>
    /// 【图书】仓储
    /// </summary>
    public class BookRepository(IDbContextProvider<BookStoreDbContext> dbContextProvider)
        : EfCoreRepository<BookStoreDbContext, Book, Guid>(dbContextProvider),
          IBookRepository
    {
        public override async Task<IQueryable<Book>> WithDetailsAsync()
        {
            return (await GetQueryableAsync()).IncludeDetails();
        }

        public override async Task<Book> GetAsync(Guid id, bool includeDetails = true, CancellationToken cancellationToken = new())
        {
            var entity = await FindAsync(id, includeDetails, GetCancellationToken(cancellationToken));
            return entity switch
            {
                null => throw new BookNotFoundException(id),
                _ => entity
            };
        }

        public new async Task<Book> GetAsync(Expression<Func<Book, bool>> predicate, bool includeDetails = true, CancellationToken cancellationToken = new())
        {
            var entity = await FindAsync(predicate, includeDetails, GetCancellationToken(cancellationToken));
            return entity switch
            {
                null => throw new BookNotFoundException(),
                _ => entity
            };
        }
    }

    // 自定义异常类
    public class BookNotFoundException : Exception
    {
        public BookNotFoundException(Guid id)
            : base($"Book with ID {id} not found.")
        {
        }

        public BookNotFoundException()
            : base("Book not found.")
        {
        }
    }
}