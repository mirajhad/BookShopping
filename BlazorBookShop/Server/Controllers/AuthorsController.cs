using AutoMapper;
using BlazorBookShop.Server.Data;
using BlazorBookShop.Server.Helpers;
using BlazorBookShop.Server.UtilityServices;
using BlazorBookShop.Shared.DTOs;
using BlazorBookShop.Shared.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazorBookShop.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IUtilityService _utilityService;
        private IMapper _mapper;

        public AuthorsController(ApplicationDbContext context,
            IUtilityService utilityService,
            IMapper mapper)
        {
            _context = context;
            _utilityService = utilityService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<List<Author>>> GetAuthors([FromQuery] PageInfoDTO pageInfo)
        {
            var Authors = _context.Authors.AsQueryable();
            await HttpContext.AddInResponse(Authors, pageInfo.PageSize);

            return await Authors.Paging(pageInfo).ToListAsync();

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Author>> GetAuthor(int id)
        {
            var author = await _context.Authors.FirstOrDefaultAsync(x => x.Id == id);
            if (author == null) { return NotFound(); }
            return author;
        }

        [HttpGet("search/{Name}")]
        public async Task<ActionResult<List<Author>>> SearchByName(string Name)
        {
            if (string.IsNullOrEmpty(Name) || string.IsNullOrWhiteSpace(Name)) { return NotFound(); }
            return await _context.Authors.Where(x => x.Name.Contains(Name))
                .Take(5)
                .ToListAsync();
        }
        [HttpPost]
        public async Task<ActionResult<int>> PostAuthor(Author author)
        {
            if (!string.IsNullOrEmpty(author.AuthorImage) || !string.IsNullOrWhiteSpace(author.AuthorImage))
            {

                var image = Convert.FromBase64String(author.AuthorImage);
                author.AuthorImage = await _utilityService.SaveImage(image, "jpg", "AuthorImage");

            }
            await _context.Authors.AddAsync(author);
            await _context.SaveChangesAsync();
            return author.Id;
        }
        [HttpPut]
        public async Task<ActionResult> PutAuthor(Author author)
        {
            var authorFromDb = await _context.Authors.FirstOrDefaultAsync(x => x.Id == author.Id);
            if (authorFromDb == null) { return NotFound(); }
            authorFromDb = _mapper.Map(author, authorFromDb);

            if (!string.IsNullOrEmpty(author.AuthorImage) || !string.IsNullOrWhiteSpace(author.AuthorImage))
            {

                var image = Convert.FromBase64String(author.AuthorImage);
                authorFromDb.AuthorImage = await _utilityService.EditImage(image, "jpg", "AuthorImage", authorFromDb.AuthorImage);

            }
            await _context.SaveChangesAsync();
            return NoContent();

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAuthor(int id)
        {
            var author = await _context.Authors.FirstOrDefaultAsync(x => x.Id == id);
            if (author == null) { return NotFound(); }
            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();
            return NoContent();





        }
    }
}
