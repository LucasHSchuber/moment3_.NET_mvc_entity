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

        public string? AuthorName { get; set; }

        public string? ImageName { get; set; }

        [NotMapped]
        // [Display(Name = "Bild")]
        public IFormFile? ImageFile { get; set; }



        //lägg till genre och betyg och beskrivning
    }


}