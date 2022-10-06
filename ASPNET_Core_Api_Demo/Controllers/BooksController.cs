using ASPNET_Core_Books_Api_Demo.Models;
using ASPNET_Core_Books_Api_Demo.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ASPNET_Core_Books_Api_Demo.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBooksRepository _BooksRepository;
        public BooksController(IBooksRepository booksRepository)
        {
            _BooksRepository = booksRepository;
        }
        [Authorize(Roles ="Admin")]
        [HttpGet("")]
        public async Task<IActionResult> GetAllBooks()
        {
            var books = await _BooksRepository.GetAllBooksAsync();

            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById([FromRoute] int id)
        {
            var book = await _BooksRepository.GetBookByIdAsync(id);

            return Ok(book);
        }

        [HttpPost("")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddBook([FromBody] BookModel bookModl)
        {
            var id = await _BooksRepository.AddBookAsync(bookModl);
            return CreatedAtAction(nameof(GetBookById), new { Id = id, Controller = "books" }, id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook([FromBody] BookModel bookModl, [FromRoute] int id)
        {
            await _BooksRepository.UpdateBookAsync(bookModl, id);
            return CreatedAtAction(nameof(GetBookById), new { Id = id, Controller = "books" }, bookModl);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateBookProperty([FromBody] JsonPatchDocument document, [FromRoute] int id)
        {
            await _BooksRepository.UpdateBookPatchAsync(document, id);
            return Ok(id);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook([FromRoute] int id)
        {
            await _BooksRepository.DeleteBookAsync(id);
            return Ok($"Book No. {id} Deleted Successfully");
        }
    }
}
