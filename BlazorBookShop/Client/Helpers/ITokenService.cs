using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BlazorBookShop.Client.Helpers
{
    public interface ITokenService
    {
        IEnumerable<Claim> GetClaims(string token);

    }
}
