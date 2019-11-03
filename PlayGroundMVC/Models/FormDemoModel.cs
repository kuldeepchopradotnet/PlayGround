using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PlayGroundMVC.Models
{
    public class FormDemoModel
    {
        //https://www.tutorialsteacher.com/mvc/implement-validation-in-asp.net-mvc
        // apply data annotation attribute like required
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        // Display custom message
        [Required(ErrorMessage ="Password is required")]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d).{8,15}$",ErrorMessage ="Not a valid password")]
        public string Password { get; set; }
    }
}