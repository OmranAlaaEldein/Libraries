using AutoMapper;
using Libraries.Models;
using Libraries.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Libraries
{
    public class UserProfile: Profile
    {
        public UserProfile()
        {
            CreateMap<BookCreateUpdateDto, Book>();
            CreateMap<Book, BookDto>();
            
            CreateMap<Author, AuthorDto>();
            CreateMap<AuthorDto, Author>();
            
            CreateMap<Library, LibraryDto>();
            CreateMap<LibraryDto, Library>();
        }
    }
}
