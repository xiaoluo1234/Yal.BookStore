using Yal.BookStore.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace Yal.BookStore.Web.Pages;

/* Inherit your PageModel classes from this class.
 */
public abstract class BookStorePageModel : AbpPageModel
{
    protected BookStorePageModel()
    {
        LocalizationResourceType = typeof(BookStoreResource);
    }
}
