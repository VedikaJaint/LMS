using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
#nullable disable

namespace LMS.Models
{
    public class Author
    {

        [Key]
        public int AuthorId { get; set; }
        public string AuthorName { get; set; }

       
    }
}
