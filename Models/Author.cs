using System;
using System.ComponentModel.DataAnnotations;

namespace moment3_mvc_entity.Models
{

    public class Author
    {

        //properties
        public int AuthorId { get; set; }
        public string? Name { get; set; }

        // Navigation property
        public List<Book>? Books { get; set; }

    }


}