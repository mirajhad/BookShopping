using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechnologyKeeda.WebAPI.Models;

namespace TechnologyKeeda.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        // Verb             FromQuery                              FromBody
        // HttpGet          inbuilt types, complex types               NA
        // HttpPost            inbuilt types                         Complex Types 
        // HttpPut
        //HttpDelete



     //  public static List<Category> categoriesList = new List<Category>();

        [HttpGet]
        public IActionResult GetCategories() {
            //if (categoriesList.Count == 0)
            //{
            //    categoriesList.Add(new Category { Id = 1, Name = "Tech" });
            //    categoriesList.Add(new Category { Id = 2, Name = "News" });
            //    categoriesList.Add(new Category { Id = 3, Name = "Media" });
            //    categoriesList.Add(new Category { Id = 4, Name = "Blog" });
            //}
            //return Ok(categoriesList);
           return Ok();

        }
        [HttpPost]
        public IActionResult PostCategories([FromBody]Category category)
        {
           // categoriesList.Add(category);
            return Ok();

        }

        [HttpPut("{id}")]
        public IActionResult PutCategories(int id,Category category) {
        
          //var categoryFromList = categoriesList.FirstOrDefault(x=>x.Id==id);
          //  categoryFromList.Id = id;
          //  categoryFromList.Name = category.Name;
            return Ok(/*categoryFromList*/);

        }
        [HttpDelete("{id}")]
        public IActionResult DeleteCategories(int id)
        {

            //var categoryFromList = categoriesList.FirstOrDefault(x => x.Id == id);
            //categoriesList.Remove(categoryFromList);
            return Ok();

        }





    }
    
}
