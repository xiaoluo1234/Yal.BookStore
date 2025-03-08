using Volo.Abp.Modularity;

namespace Yal.BookStore;

public abstract class BookStoreApplicationTestBase<TStartupModule> : BookStoreTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
