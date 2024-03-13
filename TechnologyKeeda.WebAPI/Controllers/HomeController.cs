using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using TechnologyKeeda.WebAPI.Utility;

namespace TechnologyKeeda.WebAPI.Controllers
{

    // Verbs :  Get, Post, Put, Delete



    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        protected IConfiguration _config;
        private SchoolSettings _schoolSettings;

        public HomeController(IConfiguration config,
            IOptions<SchoolSettings> schoolSettings)
        {
            _config = config;
            _schoolSettings = schoolSettings.Value;
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_schoolSettings.Address.City);
        }
    }
}
