using BlazorBookShop.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorBookShop.Client.Helpers
{
    public interface IAuthenticationService
    {
        Task Login(DtoToSend tokenData);
        Task Logout();

    }
}
