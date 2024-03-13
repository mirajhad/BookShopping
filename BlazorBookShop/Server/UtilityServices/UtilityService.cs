using BlazorBookShop.Shared.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BlazorBookShop.Server.UtilityServices
{
    public class UtilityService : IUtilityService
    {
        private readonly IWebHostEnvironment _env;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly UserManager<IdentityUser> _userManager;
        private SymmetricSecurityKey _key;

        public UtilityService(IWebHostEnvironment env, IHttpContextAccessor contextAccessor, 
            UserManager<IdentityUser> userManager, IConfiguration config)
        {
            _env = env;
            _contextAccessor = contextAccessor;
            _userManager = userManager;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:Key"]));

        }

        public Task DeleteImage(string filePath, string containerName)
        {
            var fileName =  Path.GetFileName(filePath);
            string pathFile=  Path.Combine(_env.WebRootPath,containerName,fileName);
            if (File.Exists(pathFile)) { 
            File.Delete(pathFile);
            }
            return Task.CompletedTask;
        }

        public async Task<string> EditImage(byte[] imageContent, string ImageExtension, string containerName, string filePath)
        {
          if(!string.IsNullOrEmpty(filePath))
            {
                await DeleteImage(filePath, containerName);

            }
            return await SaveImage(imageContent, ImageExtension, containerName);
        }

        public async Task<DtoToSend> GenerateToken(RegisterDto registerDto)
        {
            var claims = new List<Claim>()
           {
               new Claim(ClaimTypes.Name,registerDto.Email),
               new Claim(ClaimTypes.Email,registerDto.Email),

           };
            var identityUser =  await _userManager.FindByEmailAsync(registerDto.Email);
            var claimsinDb =  await _userManager.GetClaimsAsync(identityUser);
            claims.AddRange(claimsinDb);
            var hasedKey = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256);
            var expirationTime = DateTime.UtcNow.AddDays(7);

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: claims,
                expires: expirationTime,
                signingCredentials: hasedKey);

            return new DtoToSend()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                ExpireTime = expirationTime,
            };

        }

        public async Task<string> SaveImage(byte[] imageContent, string ImageExtension, string containerName)
        {
            var fileName = $"{Guid.NewGuid()}.{ImageExtension}";
            string PathDirectory = Path.Combine(_env.WebRootPath, containerName);
            if(!Directory.Exists(PathDirectory))
            {
                Directory.CreateDirectory(PathDirectory);
            }
            string fullPath =  Path.Combine(PathDirectory,fileName);
            await File.WriteAllBytesAsync(fullPath, imageContent);

            var filePath = $"{_contextAccessor.HttpContext.Request.Scheme}://{_contextAccessor.HttpContext.Request.Host}";
            var dbPath =  Path.Combine(filePath,containerName, fileName).Replace("\\","/");
            return dbPath;



        }
    }
}
