using System.ComponentModel.DataAnnotations;

namespace Exam_C.Models
{
    public class Invitation
    {
        [Key]
        public int InvitationId {get;set;}
        public int UserId {get;set;}
        public int ActivityId {get;set;}

        public User NavUser {get;set;}
        public ActivityCenter NavActivity {get;set;}
    }
}