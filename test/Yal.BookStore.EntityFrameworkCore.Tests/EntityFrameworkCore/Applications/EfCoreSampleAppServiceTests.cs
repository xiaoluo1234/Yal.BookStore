using Yal.BookStore.Samples;
using Xunit;

namespace Yal.BookStore.EntityFrameworkCore.Applications;

[Collection(BookStoreTestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<BookStoreEntityFrameworkCoreTestModule>
{

}
