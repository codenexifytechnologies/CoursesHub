using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CoursesHub.Models
{
    public class ContactUs
    {
        [Required(ErrorMessage = "The Name field is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The Email field is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "The Subject field is required")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "The Message field is required")]
        public string Message { get; set; }
    }
}