using ASPNET_Core_Books_Api_Demo.Data;
using ASPNET_Core_Books_Api_Demo.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNET_Core_Books_Api_Demo.Repository
{
    public class BooksRepository : IBooksRepository
    {
        private readonly BooksDbContext _Context;
        public BooksRepository(BooksDbContext DbContext)
        {
            _Context = DbContext;
        }

        public async Task<List<BookModel>> GetAllBooksAsync()
        {
            var books = await _Context.Books_Tbl.Select(x => new BookModel()
            {
                Id = x.Id,
                Name = x.Name,
                Author = x.Author,
                Description = x.Description
            }).ToListAsync();

            return books;
        }
        
        public async Task<BookModel> GetBookByIdAsync(int Id)
        {
            var books = await _Context.Books_Tbl.Select(x => new BookModel()
            {
                Id = x.Id,
                Name = x.Name,
                Author = x.Author,
                Description = x.Description
            }).Where(x => x.Id == Id).FirstOrDefaultAsync();

            return books;
        }

        public async Task<int> AddBookAsync(BookModel bookModl)
        {
            var book = new Books()
            {
                Name = bookModl.Name,
                Author = bookModl.Author,
                Description = bookModl.Description
            };
            _Context.Books_Tbl.Add(book);
            await _Context.SaveChangesAsync();

            return book.Id;
        }
    }
}
