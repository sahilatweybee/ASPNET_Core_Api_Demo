using ASPNET_Core_Books_Api_Demo.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ASPNET_Core_Books_Api_Demo.Data
{
    public class BooksDbContext : IdentityDbContext<AppUser>
    {
        public BooksDbContext(DbContextOptions<BooksDbContext> options)
            : base(options)
        {

        }
        public DbSet<Books> Books_Tbl { get; set; }
    }
}
