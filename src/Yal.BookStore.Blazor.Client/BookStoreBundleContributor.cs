﻿using Volo.Abp.AspNetCore.Mvc.UI.Bundling;

namespace Yal.BookStore.Blazor.Client;

/* Add your global styles/scripts here.
 * See https://abp.io/docs/latest/framework/ui/blazor/global-scripts-styles to learn how to use it
 */
public class BookStoreBundleContributor : BundleContributor
{
    public override void ConfigureBundle(BundleConfigurationContext context)
    {
        context.Files.Add(new BundleFile("main.css", true));
    }
}
