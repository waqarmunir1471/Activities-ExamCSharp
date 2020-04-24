using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Exam_C.Models {
    public class User {
        [Key]
        public int UserId { get; set; }

        [Required (ErrorMessage = "First Name Must be Entered")]
        [Display (Name = "First Name : ")]
        [MinLength (2)]
        public string FirstName { get; set; }

        [Required (ErrorMessage = "Last Name Must be Entered")]
        [Display (Name = "Last Name : ")]
        [MinLength (2)]
        public string LastName { get; set; }

        [Required]
        [DataType (DataType.Date)]
        [Display (Name = "Enter Youtr Date of Birth")]
        [DOB]
        public DateTime DOB { get; set; }

        [Required (ErrorMessage = "Email Must be Entered")]
        [Display (Name = "Email  : ")]
        [DataType (DataType.EmailAddress)]
        public string Email { get; set; }

        [Required (ErrorMessage = "Password Must be Entered")]
        [Display (Name = "Password  : ")]
        [DataType (DataType.Password)]
        [MinLength(8)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$",ErrorMessage="Password Must Contains 1 Number, 1 Letter and 1 Special Character atleast")]
        public string Password { get; set; }

        [Display (Name = "Confirm Password  : ")]
        [DataType (DataType.Password)]
        [Compare ("Password")]
        [NotMapped]
        public string ConfirmPassword { get; set; }
        public List<ActivityCenter> MyActivity{get; set;}
        public List<Invitation> MyInvitations{get; set;}
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

    }
}