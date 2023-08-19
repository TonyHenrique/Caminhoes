using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TonyWebApplication
{
    public static class NavigationManagerUtils
    {
        public static string? GetQueryValue(this NavigationManager NavigationManager, string QueryName)
        {
            var uri = NavigationManager.ToAbsoluteUri(NavigationManager.Uri);

            if (QueryHelpers.ParseQuery(uri.Query).TryGetValue(QueryName, out var param))
            {
                return param.FirstOrDefault();
            }

            return null;
        }
    }
}
