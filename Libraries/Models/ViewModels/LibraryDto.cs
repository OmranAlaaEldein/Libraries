using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Libraries.Models.ViewModels
{
    public class LibraryDto
    {
        
        public string Name { set; get; }

        public string Address { set; get; }
        
        [Timestamp]
        public DateTime OpenTime { set; get; }

        [Timestamp]
        public DateTime CloseTime { set; get; }

        public List<BookDto> Books { set; get; }
    }
}
