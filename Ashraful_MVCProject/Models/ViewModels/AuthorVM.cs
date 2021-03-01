using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ashraful_MVCProject.Models.ViewModels
{
    public class AuthorVM
    {
        public int AuthorID { get; set; }

        [Required(ErrorMessage = "Required")]


        public string AuthorName { get; set; }

        [Required(ErrorMessage = "Required")]

        public string Email { get; set; }

        [Required(ErrorMessage = "Required")]


        public int Phone { get; set; }


        [Required(ErrorMessage = "Required")]

        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "Required")]


        public string Address { get; set; } 
    }
}