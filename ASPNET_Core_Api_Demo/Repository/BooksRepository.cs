using ASPNET_Core_Books_Api_Demo.Data;
using ASPNET_Core_Books_Api_Demo.Models;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ASPNET_Core_Books_Api_Demo.Repository
{
    public class BooksRepository : IBooksRepository
    {
        private readonly BooksDbContext _Context;
        private readonly IMapper _Mapper;
        public BooksRepository(BooksDbContext DbContext, IMapper mapper)
        {
            _Context = DbContext;
            _Mapper = mapper;
        }

        public async Task<List<BookModel>> GetAllBooksAsync()
        {
            var books = await _Context.Books_Tbl.ToListAsync();
            return _Mapper.Map<List<BookModel>>(books);
        }

        public async Task<BookModel> GetBookByIdAsync(int Id)
        {
            //var books = await _Context.Books_Tbl.Select(x => new BookModel()
            //{ 
            //    Id = x.Id,
            //    Name = x.Name,
            //    Author = x.Author,
            //    Description = x.Description
            //}).Where(x => x.Id == Id).FirstOrDefaultAsync();

            //return books;

            var book = await _Context.Books_Tbl.FindAsync(Id);
            return _Mapper.Map<BookModel>(book);
        }

        public async Task<int> AddBookAsync(BookModel bookModl)
        {
            var book = _Mapper.Map<Books>(bookModl);
            _Context.Books_Tbl.Add(book);
            await _Context.SaveChangesAsync();

            return book.Id;
        }

        public async Task UpdateBookAsync(BookModel bookModl, int bookId)
        {
            var book = new Books()
            {
                Id = bookId,
                Name = bookModl.Name,
                Author = bookModl.Author,
                Description = bookModl.Description
            };

            _Context.Books_Tbl.Update(book);
            await _Context.SaveChangesAsync();
        }

        public async Task UpdateBookPatchAsync(JsonPatchDocument document, int bookId)
        {
            var book = await _Context.Books_Tbl.FindAsync(bookId);
            if (book != null)
            {
                document.ApplyTo(book);
                await _Context.SaveChangesAsync();
            }
        }

        public async Task DeleteBookAsync(int bookId)
        {
            var book = new Books() { Id = bookId };
            _Context.Books_Tbl.Remove(book);
            await _Context.SaveChangesAsync();
        }
    }
}
