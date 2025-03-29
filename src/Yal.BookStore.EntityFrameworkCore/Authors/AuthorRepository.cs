using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Yal.BookStore.EntityFrameworkCore;

namespace Yal.BookStore.Authors
{
    /// <summary>
    /// 【作者】仓储
    /// </summary>
    public class AuthorRepository(IDbContextProvider<BookStoreDbContext> dbContextProvider)
        : EfCoreRepository<BookStoreDbContext, Author, Guid>(dbContextProvider),
          IAuthorRepository
    {
        public override async Task<IQueryable<Author>> WithDetailsAsync()
        {
            return (await GetQueryableAsync()).IncludeDetails();
        }

        public override async Task<Author> GetAsync(Guid id, bool includeDetails = true, CancellationToken cancellationToken = new())
        {
            var entity = await FindAsync(id, includeDetails, GetCancellationToken(cancellationToken));
            return entity switch
            {
                null => throw new AuthorNotFoundException(id),
                _ => entity
            };
        }

        public new async Task<Author> GetAsync(Expression<Func<Author, bool>> predicate, bool includeDetails = true, CancellationToken cancellationToken = new())
        {
            var entity = await FindAsync(predicate, includeDetails, GetCancellationToken(cancellationToken));
            return entity switch
            {
                null => throw new AuthorNotFoundException(),
                _ => entity
            };
        }

        public async Task<Dictionary<string, string>> GetAuthorCodeNameDic(List<string> codes)
        {
            return await (await GetQueryableAsync())
                .Where(x => codes.Contains(x.Code!))
                .ToDictionaryAsync(x => x.Code!, x => x.Name!);
        }
    }

    // 自定义异常类
    public class AuthorNotFoundException : Exception
    {
        public AuthorNotFoundException(Guid id)
            : base($"Author with ID {id} not found.")
        {
        }

        public AuthorNotFoundException()
            : base("Author not found.")
        {
        }
    }
}
