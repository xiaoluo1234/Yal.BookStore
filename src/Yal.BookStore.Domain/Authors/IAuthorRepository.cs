using System;
using Volo.Abp.Domain.Repositories;

namespace Yal.BookStore.Authors;
public interface IAuthorRepository : IRepository<Author, Guid>
{

}