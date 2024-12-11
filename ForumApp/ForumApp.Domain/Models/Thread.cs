namespace ForumApp.Domain.Models
{
    public class Thread : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }

        // Foreign Keys
        public int UserId { get; set; }
        public User User { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        // Navigation Properties
        public ICollection<Post> Posts { get; set; } = new List<Post>();

    }
}
