using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorBookShop.Client.StringHelpers
{
    public static class JavaScriptExtensions
    {
        public static async ValueTask<bool> Confirm(this IJSRuntime js,string message)
        {
            return await js.InvokeAsync<bool>("confirm", message);
        }
        public static async ValueTask WelComeMessage(this IJSRuntime js,string message)
        {
           await  js.InvokeVoidAsync("WelcomeMsg", message);
        }

        public static async ValueTask<object> SetTokenInLocalStorage(this IJSRuntime js, string key,string value)
        {
            return await js.InvokeAsync<object>("localStorage.setItem", key,value);
        }
        public static async ValueTask<string> GetTokenInLocalStorage(this IJSRuntime js, string key)
        {
            return await js.InvokeAsync<string>("localStorage.getItem", key);
        }
        public static async ValueTask<object> RemoveTokenInLocalStorage(this IJSRuntime js, string key)
        {
            return await js.InvokeAsync<object>("localStorage.removeItem", key);
        }



    }
}
