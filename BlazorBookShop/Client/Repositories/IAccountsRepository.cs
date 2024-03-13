using BlazorBookShop.Shared.DTOs;
using BlazorBookShop.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorBookShop.Client.Repositories
{
    public interface IAccountsRepository
    {
        Task<DtoToSend> RegisterUser(RegisterDto registerDto);  
        Task<DtoToSend> LoginUser(RegisterDto registerDto);
        Task<Pagination<List<UserDTO>>> GetAllUsers(PageInfoDTO pageInfoDTO);
        Task<List<RoleDTO>> GetAllRoles();
        Task AddRole(UserRoleDTO roleDto);
        Task DeleteRole(UserRoleDTO roleDto);

    }
}
