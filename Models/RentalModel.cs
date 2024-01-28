using System;
using System.ComponentModel.DataAnnotations;

namespace moment3_mvc_entity.Models
{

    public class RentalModel
    {

        //properties

        public string? Name { get; set; }

        [MinLength(10)]
        [MaxLength(10)]
        public int? IdNumber { get; set; }

        public DateTime RentalDate { get; set; }
        public string? BookTitle { get; set; }

    }


}