using Microsoft.EntityFrameworkCore;

namespace Exam_C.Models
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions options) : base(options){}
        public DbSet<User> Users {get;set;}
        public DbSet<ActivityCenter> Activities {get;set;}
        public DbSet<Invitation> Invitations {get;set;}
    }
}