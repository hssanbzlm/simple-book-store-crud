using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Models
{
    public class Author
    {
        public int Id { get; set; }
        [Required]
        [StringLength(20,MinimumLength =3)]
        public string FullName { get; set; }

        public Author()
        {

            this.Id = 0;
            this.FullName = "Not yet assigned";
            


        }

    }
}
