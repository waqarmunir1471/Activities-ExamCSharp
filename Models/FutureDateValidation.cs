using System;
using System.ComponentModel.DataAnnotations;

namespace Exam_C.Models
{
    public class FutureDateAttribute : ValidationAttribute {
        protected override ValidationResult IsValid (object value, ValidationContext validationContext) {
            DateTime WeddingDate = (DateTime) value;
            if (WeddingDate < DateTime.Now) 
            {
                return new ValidationResult ("Please Enter a Valid Future Date and Time!");
            } 
            else 
            {
                return ValidationResult.Success;
            }
        }

    }
}