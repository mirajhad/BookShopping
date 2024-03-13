using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorBookShop.Client.Helpers
{
    public interface IUtilityService
    {
        ValueTask ShowMessage(string title,string message,string icon);
        ValueTask ShowSuccessMessage(string Message);
        ValueTask ShowErrorMessage(string Message);
    }
}
