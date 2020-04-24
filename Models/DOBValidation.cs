using System;
using System.ComponentModel.DataAnnotations;

namespace Exam_C.Models {
    public class DOBAttribute : ValidationAttribute {
        protected override ValidationResult IsValid (object value, ValidationContext validationContext) {
            DateTime DOB = (DateTime) value;
            DateTime today = DateTime.Today;
            int age = today.Year - DOB.Year;
            if (DOB > DateTime.Now ) {
                return new ValidationResult ("DOB Must be In Past!");
            } else if (age < 18){
                return new ValidationResult ("You must be 18!");
            }
            else if (DOB > DateTime.Now ) {
                return new ValidationResult ("DOB Must be In Past!");
            }
            else {
                    return ValidationResult.Success;
                }
        }

    }
}