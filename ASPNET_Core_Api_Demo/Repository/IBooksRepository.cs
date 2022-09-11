using ASPNET_Core_Books_Api_Demo.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ASPNET_Core_Books_Api_Demo.Repository
{
    public interface IBooksRepository
    {
        Task<int> AddBookAsync(BookModel bookModl);
        Task<List<BookModel>> GetAllBooksAsync();
        Task<BookModel> GetBookByIdAsync(int Id);
    }
}