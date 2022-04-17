using AutoMapper;
using Libraries.Models;
using Libraries.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Libraries
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<Library, LibraryDto>();
            CreateMap<LibraryDto, Library>();
            CreateMap<LibraryCreateUpdateDto, Library>();

            CreateMap<BookDto, Book>();
            CreateMap<Book, BookDto>();
            CreateMap<BookCreateUpdateDto, Book>();


            CreateMap<AuthorDto, Author>();
            CreateMap<Author, AuthorDto>();

        }
    }
}
