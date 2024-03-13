using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BlazorBookShop.Server.Helpers
{
    public static  class ExtensionForContext
    {
     public async static Task AddInResponse<T>(this HttpContext context,
         IQueryable<T> querable,int pageSize)
        {
            int count = await querable.CountAsync();
            int TotalPages = (int)Math.Ceiling((double)count / pageSize);
            context.Response.Headers.Add("totalPages", TotalPages.ToString());
        }

    }
}
