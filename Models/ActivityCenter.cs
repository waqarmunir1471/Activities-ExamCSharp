using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Exam_C.Models {
    public class ActivityCenter {
        [Key]
        public int ActivityId { get; set; }
        [Required(ErrorMessage="Title Must be Entered")]
        [Display(Name="Title : ")]
        public string ActivityTitle { get; set; }

        [DataType(DataType.Date)]
        [FutureDate]
        [Required(ErrorMessage="Date Must be Entered")]
        [Display(Name="Date : ")]
        public DateTime ActivityDate { get; set; }
        [Required(ErrorMessage="Time Must be Entered")]
        [DataType(DataType.Time)]
        [Display(Name="Time : ")]
        public DateTime ActivityTime { get; set; }
        [Display(Name="Duration : ")]

        public int ActivityDuration {get;set;}
        public string Unit {get;set;}
        [Required(ErrorMessage="Description Must be Entered")]
        [Display(Name="Description Must be Entered : ")]
        public string ActivityDescription {get;set;}
        public int UserId {get;set;}
        public User ActivityCreator {get;set;}
        public List<Invitation> Participants {get; set;}

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

    }
}