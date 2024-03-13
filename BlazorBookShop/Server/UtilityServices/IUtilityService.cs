using BlazorBookShop.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorBookShop.Server.UtilityServices
{
    public interface IUtilityService
    {
        Task<string> SaveImage(byte[] imageContent, string ImageExtension, string containerName);
        Task<string> EditImage(byte[] imageContent, string ImageExtension, string containerName,string filePath);
        Task DeleteImage(string filePath,string containerName);
        Task<DtoToSend> GenerateToken(RegisterDto registerDto);




    }
}
