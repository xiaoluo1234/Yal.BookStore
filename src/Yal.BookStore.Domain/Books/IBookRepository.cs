using System;
using Volo.Abp.Domain.Repositories;

namespace Yal.BookStore.Books;
public interface IBookRepository : IRepository<Book, Guid>
{

}