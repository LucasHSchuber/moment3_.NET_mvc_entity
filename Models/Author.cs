using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace moment3_mvc_entity.Models
{

    public class Author
    {

        //properties
        public int AuthorId { get; set; }
        public string? Name { get; set; }
        public string? Bio { get; set; }

        public List<Book>? Books { get; set; }


    }


}