//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Ashraful_MVCProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Book
    {
        public int BookID { get; set; }

        [Required(ErrorMessage ="Required")]
        public string BookName { get; set; }
        [Required(ErrorMessage = "Required")]

        public Nullable<decimal> BookPrice { get; set; }
        [Required(ErrorMessage = "Required")]

        public string BookEdition { get; set; }
        [Required(ErrorMessage = "Required")]

        public string Description { get; set; }
        public Nullable<int> AuthorID { get; set; }
        public Nullable<int> PublisherID { get; set; }
    
        public virtual Author Author { get; set; }
        public virtual Publisher Publisher { get; set; }
    }
}