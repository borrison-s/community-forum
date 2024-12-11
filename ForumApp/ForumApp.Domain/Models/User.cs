namespace ForumApp.Domain.Models
{
    public class User : BaseEntity
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public ICollection<Thread> Threads { get; set; } = new List<Thread>();
        public ICollection<Post> Posts { get; set; } = new List<Post>();


    }
}
