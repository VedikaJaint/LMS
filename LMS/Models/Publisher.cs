using System;
using System.Collections.Generic;


using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
#nullable disable

namespace LMS.Models
{
    public partial class Publisher
    {

        [Key]
        public int PublisherId { get; set; }
        public string PublisherName { get; set; }

        
    }
}
