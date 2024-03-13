using BlazorBookShop.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorBookShop.Client.Helpers
{
    public interface IHttpService
    {
        Task<ServerResponseHelper<object>> Post<T>(string url,T Data);
        Task<ServerResponseHelper<TResponse>> PostData<T,TResponse>(string url,T Data);
        Task<ServerResponseHelper<T>> Get<T>(string url);
        Task<ServerResponseHelper<object>> Delete(string url);
        Task<ServerResponseHelper<object>> Put<T>(string url, T Data);
        Task<Pagination<T>> GetPagination<T>(string url,PageInfoDTO pageInfo);
    }
}
