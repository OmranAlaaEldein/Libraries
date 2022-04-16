using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Libraries.Models
{
    public class Library
    {
        public int Id { set; get; }

        [Required]
        [MaxLength(20)]
        public string Name { set; get; }

        public string Address { set; get; }
        
        public int TotalStorge { set; get; }

        public DateTime StartDay { set; get; }

        [Timestamp]
        public DateTime OpenTime { set; get; }

        [Timestamp]
        public DateTime CloseTime { set; get; }

        public List<Book> Books { set; get; }
    }
}
