namespace ForumApp.Domain.Models
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        // Navigation Properties
        public ICollection<Thread> Threads { get; set; } = new List<Thread>();
    }
}
