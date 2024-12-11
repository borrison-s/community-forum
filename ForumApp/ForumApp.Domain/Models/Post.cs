namespace ForumApp.Domain.Models
{
    public class Post : BaseEntity
    {
        public string Content { get; set; }

        // Foreign Keys
        public int UserId { get; set; }
        public User User { get; set; }

        public int ThreadId { get; set; }
        public Thread Thread { get; set; }
    }
}
