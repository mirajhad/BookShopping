using BlazorBookShop.Client.Helpers;
using BlazorBookShop.Shared.DTOs;
using BlazorBookShop.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorBookShop.Client.Repositories
{
    public class AccountsRepository : IAccountsRepository
    {
        private readonly IHttpService _httpService;
        private readonly string url = "api/accounts";
        private readonly string userAPIURL = "api/users";

        public AccountsRepository(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task AddRole(UserRoleDTO roleDto)
        {
            var response = await _httpService.Post($"{userAPIURL}/addrole", roleDto);
            if (!response.IsSuccess)
            {
                throw new ApplicationException(await response.GetBodyPart());
            }

        }

        public async Task DeleteRole(UserRoleDTO roleDto)
        {
            var response = await _httpService.Post($"{userAPIURL}/removerole", roleDto);
            if (!response.IsSuccess)
            {
                throw new ApplicationException(await response.GetBodyPart());
            }
        }

        public async Task<List<RoleDTO>> GetAllRoles()
        {
           var response =   await _httpService.Get<List<RoleDTO>>($"{userAPIURL}/roles");
            if(!response.IsSuccess)
            {
                throw new ApplicationException(await response.GetBodyPart());
            }
            return response.ServerResponse;
        }

        public async Task<Pagination<List<UserDTO>>> GetAllUsers(PageInfoDTO pageInfoDTO)
        {
           
                return await _httpService.GetPagination<List<UserDTO>>(userAPIURL, pageInfoDTO);
          
        }

        public async Task<DtoToSend> LoginUser(RegisterDto registerDto)
        {
            var response = await _httpService.PostData<RegisterDto, DtoToSend>($"{url}/login", registerDto);
            if (!response.IsSuccess)
            {
                throw new ApplicationException(await response.GetBodyPart());
            }
            return response.ServerResponse;
        }

        public async Task<DtoToSend> RegisterUser(RegisterDto registerDto)
        {
                 var response =  await _httpService.PostData<RegisterDto,DtoToSend>($"{url}/register", registerDto);
            if(!response.IsSuccess)
            {
                throw new ApplicationException(await response.GetBodyPart());   
            }
            return response.ServerResponse;
            
        }
    }
}
