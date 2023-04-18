namespace Assignment.Models
{
    public class AuthorBooks
    {
        public Guid Id { get; set; }
        public Guid AuthorID { get; set; }
        public Guid BookID { get; set; }
        public Book? Books { get; set; }
        public Author? Author { get; set; }
    }
}
