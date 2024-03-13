using Microsoft.AspNetCore.Mvc;

namespace TechnologyKeeda.Web.Controllers
{
    public class TestController : Controller
    {
       static int a = 0;
        //{basepath(localhost:5001}/Test/showButton
        public IActionResult ShowButton()
        {
            
            return View();
        }
        public IActionResult ClickAction()
        {
            ++a;
            return View("ShowButton",a);
        }
    }
}
