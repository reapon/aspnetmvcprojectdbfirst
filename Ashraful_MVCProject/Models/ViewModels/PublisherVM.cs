using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ashraful_MVCProject.Models.ViewModels
{
    public class PublisherVM
    {
        public int PublisherID { get; set; }
        [Required(ErrorMessage = "Required")]

        public string PublisherName { get; set; }
        [Required(ErrorMessage = "Required")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Required")]

        public Nullable<int> Phone { get; set; }
        [Required(ErrorMessage = "Required")]

        public string Address { get; set; }
    }
}