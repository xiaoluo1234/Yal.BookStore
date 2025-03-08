using Yal.BookStore.Localization;
using Volo.Abp.AspNetCore.Components;

namespace Yal.BookStore.Blazor.Client;

public abstract class BookStoreComponentBase : AbpComponentBase
{
    protected BookStoreComponentBase()
    {
        LocalizationResource = typeof(BookStoreResource);
    }
}
