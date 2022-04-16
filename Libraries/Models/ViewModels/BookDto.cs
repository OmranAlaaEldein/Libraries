using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Libraries.Models.ViewModels
{
    public class BookDto
    {
        public int Id { set; get; }

        public string Tittle { set; get; }

        public int Price { set; get; }

        public int PagesCount { set; get; }

        public string Type { set; get; }

        public LibraryDto Library { set; get; }

        public AuthorDto Author { set; get; }
    }
}
