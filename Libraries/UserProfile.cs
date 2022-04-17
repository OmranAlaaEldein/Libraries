using AutoMapper;
using Libraries.Models;
using Libraries.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Libraries
{
    public class UserProfile:Profile
    {
        public UserProfile()
        {
            CreateMap<Library,LibraryDto>();
            CreateMap<LibraryCreateUpdateDto,Library>();

            CreateMap<BookDto, Book>();
            CreateMap<Book, BookDto>();
            
            CreateMap<AuthorDto, Author>();
            CreateMap<Author, AuthorDto>();
        }
    }
}
