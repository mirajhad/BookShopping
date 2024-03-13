using BlazorBookShop.Server.UtilityServices;
using BlazorBookShop.Shared.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlazorBookShop.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private IUtilityService _utilityService;

        public AccountsController(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IUtilityService utilityService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _utilityService = utilityService;
        }
        [HttpPost("register")]
        public async Task<ActionResult<DtoToSend>> Register(RegisterDto registerDto)
        {
            var user =  new IdentityUser { UserName=registerDto.Email, Email=registerDto.Email};
            var result =  await _userManager.CreateAsync(user,registerDto.Password);
            if(result.Succeeded)
            {

                var response = await _utilityService.GenerateToken(registerDto);
                return response;
            }
            else
            {
                return BadRequest("UserName & Password Invalid");
            }

        }

        [HttpPost("login")]
        public async Task<ActionResult<DtoToSend>> Login(RegisterDto registerDto)
        {
            var result = await _signInManager.PasswordSignInAsync(registerDto.Email,
                registerDto.Password, isPersistent: false,
                lockoutOnFailure: false);
            if (result.Succeeded)
            {
                var response = await _utilityService.GenerateToken(registerDto);
                return response;
            }
            else
            {
                return BadRequest("UserName & Password Invalid");
            }
        }
    }
}
