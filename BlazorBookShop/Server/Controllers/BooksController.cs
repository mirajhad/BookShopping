using BlazorBookShop.Server.Data;
using BlazorBookShop.Server.UtilityServices;
using BlazorBookShop.Shared.DTOs;
using BlazorBookShop.Shared.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazorBookShop.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
    public class BooksController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IUtilityService _utilityService;

        public BooksController(ApplicationDbContext context, IUtilityService utilityService)
        {
            _context = context;
            _utilityService = utilityService;
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<List<Book>>> GetBooks()
        {
            var books =await  _context.Books.ToListAsync();
            return books;
        }
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<DetailsBookDTO>> GetBookById(int id)
        {
            var book = await _context.Books.Include(x=>x.BookCategories)
                .ThenInclude(y=>y.Category)
                .Include(a=>a.BookAuthors).ThenInclude(b=>b.Author)
                .FirstOrDefaultAsync(c=>c.Id==id);

            var model = new DetailsBookDTO();
            model.Book = book;
            model.Categories = book.BookCategories.Select(x => x.Category).ToList();
            model.Authors = book.BookAuthors.Select(x => x.Author).ToList();
            return model;
        }
        [HttpPost]
        public async Task<ActionResult<int>> PostBook(Book book)
        {
            if (!string.IsNullOrEmpty(book.BookImage) || !string.IsNullOrWhiteSpace(book.BookImage))
            {

                var image = Convert.FromBase64String(book.BookImage);
                book.BookImage = await _utilityService.SaveImage(image, "jpg", "BookImage");

            }
            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
            return book.Id;
        }
        [HttpPut]
        public async Task<ActionResult> PutBook(Book book)
        {
            var bookFromDb =  await _context.Books.FirstOrDefaultAsync(x=>x.Id==book.Id);
            bookFromDb.Title = book.Title;
            await _context.Database.ExecuteSqlInterpolatedAsync(
                $"delete from BookCategories where BookId={book.Id};delete from BookAuthors where BookId={book.Id}");
           
            if (!string.IsNullOrEmpty(book.BookImage) || !string.IsNullOrWhiteSpace(book.BookImage))
            {

                var image = Convert.FromBase64String(book.BookImage);
                bookFromDb.BookImage = await _utilityService.EditImage(image, "jpg", "BookImage", bookFromDb.BookImage);

            }


            bookFromDb.BookCategories = book.BookCategories;
            bookFromDb.BookAuthors = book.BookAuthors;
            await _context.SaveChangesAsync();
            return NoContent();

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBook(int id)
        {
            var book = await _context.Books.FirstOrDefaultAsync(x => x.Id == id);
            if(book==null)
            {
                return NotFound();
            }
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
