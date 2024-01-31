using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;


namespace moment3_mvc_entity.Models
{

    public class Rental
    {

        //properties
        public int RentalId { get; set; }
        public DateTime RentDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public bool IsReturned { get; set; }
        public string? RenterName { get; set; }
        public string? RenterIdNumber { get; set; }

        public int BookId { get; set; }
        public Book? Book { get; set; } // Navigation property for the Book relationship
        
    }


}