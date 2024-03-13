using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorBookShop.Client.Helpers
{
    public class UtilityService : IUtilityService
    {
        private IJSRuntime _js;

        public UtilityService(IJSRuntime js)
        {
            _js = js;
        }

        public async ValueTask ShowErrorMessage(string Message)
        {
            await ShowMessage("Error", Message, "error");
        }

        public async ValueTask ShowMessage(string title, string message, string icon)
        {
            await _js.InvokeVoidAsync("Swal.fire", title, message, icon);
        }

        public async ValueTask ShowSuccessMessage(string Message)
        {
            await ShowMessage("Success", Message, "success");
        }
    }
}
