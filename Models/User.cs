using System;
using System.ComponentModel.DataAnnotations;

namespace moment3_mvc_entity.Models
{

    public class User
    {

        //properties

        [Key]
        public int UserId { get; set; }
        public string? Name { get; set; }
        public string? SSN { get; set; }


    }


}