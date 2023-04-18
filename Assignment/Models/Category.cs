namespace Assignment.Models
{
    public class Category
    {
        public Category()
        {
            Books = new HashSet<Book>();
        }
        public Guid CategoryID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Book>? Books { get; set; }
    }
}
