using ASPNET_Core_Books_Api_Demo.Models;
using ASPNET_Core_Books_Api_Demo.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<IActionResult> AddBook([FromBody] BookModel bookModl)
        {
            var id = await _BooksRepository.AddBookAsync(bookModl);
            return CreatedAtAction(nameof(GetBookById), new { Id = id, Controller = "books"},id);
        }
    }
}
