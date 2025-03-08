using Xunit;

namespace Yal.BookStore.EntityFrameworkCore;

[CollectionDefinition(BookStoreTestConsts.CollectionDefinitionName)]
public class BookStoreEntityFrameworkCoreCollection : ICollectionFixture<BookStoreEntityFrameworkCoreFixture>
{

}
