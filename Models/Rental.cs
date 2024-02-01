using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;


namespace moment3_mvc_entity.Models
{

    public class Rental
    {

        //properties
        public int RentalId { get; set; }

        [Required]
        [Display(Name = "Rental date")]
        public DateTime RentDate { get; set; }

        [Required]
        [Display(Name = "Return date")]
        public DateTime? ReturnDate { get; set; }

        [Required]
        [Display(Name = "Returned?")]
        public bool IsReturned { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string? RenterName { get; set; }

        [Required]
        [Display(Name = "Id number")]
        public string? RenterIdNumber { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string? Email { get; set; }

        public int BookId { get; set; }

        [Display(Name = "Book title")]
        public Book? Book { get; set; } 

    }


}