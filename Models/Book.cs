using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace moment3_mvc_entity.Models
{

    public class Book
    {

        //properties

        public int BookId { get; set; }
        public string? Title { get; set; }

        public string? Description { get; set; }

        // [MinLength(1)]
        // [MaxLength(5)]
        // [Range(0.0, 5.0)]
        public float Grade { get; set; }

        public string? Genre { get; set; }

        public string? AuthorName { get; set; }

        public string? ImageName { get; set; }

        public DateTime Created { get; set; } = DateTime.Now;

        [NotMapped]
        // [Display(Name = "Bild")]
        public IFormFile? ImageFile { get; set; }



        //lägg till genre och betyg och beskrivning
    }


}