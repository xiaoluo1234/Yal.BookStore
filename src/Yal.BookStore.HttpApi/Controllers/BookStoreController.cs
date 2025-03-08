using Yal.BookStore.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Yal.BookStore.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class BookStoreController : AbpControllerBase
{
    protected BookStoreController()
    {
        LocalizationResource = typeof(BookStoreResource);
    }
}
