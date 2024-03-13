using BlazorBookShop.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BlazorBookShop.Server.Helpers
{
    public static class ExtensionForQuerable
    {
        public static IQueryable<T> Paging<T>(this IQueryable<T> querable,PageInfoDTO pageInfo)
            {

            return querable.Skip((pageInfo.PageNumber-1)* pageInfo.PageSize)
                .Take(pageInfo.PageSize);
        }

    }
}
