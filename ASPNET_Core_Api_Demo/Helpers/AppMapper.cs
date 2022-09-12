using ASPNET_Core_Books_Api_Demo.Data;
using ASPNET_Core_Books_Api_Demo.Models;
using AutoMapper;

namespace ASPNET_Core_Books_Api_Demo.Helpers
{
    public class AppMapper : Profile
    {
        public AppMapper()
        {
            CreateMap<Books, BookModel>().ReverseMap();
        }
    }
}
