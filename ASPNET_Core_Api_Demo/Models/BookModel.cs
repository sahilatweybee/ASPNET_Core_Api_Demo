using System.ComponentModel.DataAnnotations;

namespace ASPNET_Core_Books_Api_Demo.Models
{
    public class BookModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name of the Book is Required")]
        [MinLength(3)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Name of the Author is Required")]
        [MinLength(3)]
        public string Author { get; set; }
        public string Description { get; set; }
    }
}
