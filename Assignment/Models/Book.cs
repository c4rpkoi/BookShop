namespace Assignment.Models
{
    public class Book
    {
        public Book()
        {
            AuthorBooks = new HashSet<AuthorBooks>();

        }
        public Guid ID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string? ImageUrl { get; set; }
        public int AvailableQuantity { get; set; }
        public bool IsBookOfTheWeek { get; set; }
        public Guid SupplierID { get; set; }
        public Guid CategoryID { get; set; }
        public virtual Supplier? Supplier { get; set; }
        public virtual Category? Category { get; set; }
        public virtual ICollection<AuthorBooks>? AuthorBooks { get; set; }
        
    }
}
