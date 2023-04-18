namespace Assignment.Models
{
    public class Author
    {
        public Author()
        {
            AuthorBooks = new HashSet<AuthorBooks>();
        }
        public Guid AuthorID { get; set; }
        public string Name { get; set; }
        public ICollection<AuthorBooks>? AuthorBooks { get; set; }
    }
}
