using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace moment3_mvc_entity.Models
{

    public class Book
    {

        //properties

        public int BookId { get; set; }
        public string? Title { get; set; }

        public string? Description { get; set; }

        [Range(0.0, 5.0, ErrorMessage = "Please enter a valid grade between 0.0 and 5.0.",
               ConvertValueInInvariantCulture = true)]
        public float Grade { get; set; }

        public string? Genre { get; set; }

        public string? AuthorName { get; set; }

        public string? ImageName { get; set; }

        public DateTime Created { get; set; } = DateTime.Now;

        [NotMapped]
        // [Display(Name = "Bild")]
        public IFormFile? ImageFile { get; set; }


        [ForeignKey("Author")]
        public int AuthorId { get; set; }
        public Author? Author { get; set; }
    }


}