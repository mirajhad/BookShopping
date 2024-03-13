using BlazorBookShop.Server.Data;
using BlazorBookShop.Server.Helpers;
using BlazorBookShop.Shared.DTOs;
using BlazorBookShop.Shared.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BlazorBookShop.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="Admin")]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public UsersController(ApplicationDbContext context, 
            UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        [HttpGet]
        public async Task<ActionResult<List<UserDTO>>> GetUsers([FromQuery] PageInfoDTO pageInfo)
        {
            var users = _context.Users.AsQueryable();
            await HttpContext.AddInResponse(users, pageInfo.PageSize);
            return await users.Paging(pageInfo).Select(x=>new UserDTO { Userid=x.Id,Email=x.Email}).ToListAsync();

        }
        [HttpGet("roles")]
        public async Task<ActionResult<List<RoleDTO>>> GetRoles()
        {
            return await _context.Roles.Select(x => new RoleDTO { RoleName = x.Name }).ToListAsync();

        }
        [HttpPost("addrole")]
        public async Task<ActionResult> AddRole(UserRoleDTO addRoletoUser)
        {
            var user = await _userManager.FindByIdAsync(addRoletoUser.UserId);
            await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, addRoletoUser.RoleName));
            return NoContent();
        }
        [HttpPost("removerole")]
        public async Task<ActionResult> RemoveRole(UserRoleDTO addRoletoUser)
        {
            var user = await _userManager.FindByIdAsync(addRoletoUser.UserId);
            await _userManager.RemoveClaimAsync(user, new Claim(ClaimTypes.Role, addRoletoUser.RoleName));
            return NoContent();
        }
    }
}
