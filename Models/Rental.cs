using System;
using System.ComponentModel.DataAnnotations;

namespace moment3_mvc_entity.Models
{

    public class Rental
    {

        //properties

        [Key]
        public int RentalId { get; set; }
        public string? Name { get; set; }
        public int? IdNumber { get; set; }

        public DateTime RentalDate { get; set; } = DateTime.Now;
        public DateTime ReturnDate { get; set; } = DateTime.Now.AddMonths(1);
        public string? BookTitle { get; set; }

    }


}