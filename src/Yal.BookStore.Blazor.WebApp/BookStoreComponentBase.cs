using Yal.BookStore.Localization;
using Volo.Abp.AspNetCore.Components;

namespace Yal.BookStore.Blazor.WebApp;

public abstract class BookStoreComponentBase : AbpComponentBase
{
    protected BookStoreComponentBase()
    {
        LocalizationResource = typeof(BookStoreResource);
    }
}
