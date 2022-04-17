using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Libraries.Models.ViewModels
{
    public class AuthorDto
    {
        public int Id { set; get; }

        [Required]
        [MaxLength(20)]
        public string Name { set; get; }

        [Required]
        [MaxLength(20)]
        public string LastName { set; get; }

        [DataType(DataType.EmailAddress)]
        public string Email { set; get; }

        public string Country { set; get; }

    }
}
