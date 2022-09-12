using ASPNET_Core_Books_Api_Demo.Models;
using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ASPNET_Core_Books_Api_Demo.Repository
{
    public interface IBooksRepository
    {
        Task<int> AddBookAsync(BookModel bookModl);
        Task DeleteBookAsync(int bookId);
        Task<List<BookModel>> GetAllBooksAsync();
        Task<BookModel> GetBookByIdAsync(int Id);
        Task UpdateBookAsync(BookModel bookModl, int bookId);
        Task UpdateBookPatchAsync(JsonPatchDocument document, int bookId);
    }
}