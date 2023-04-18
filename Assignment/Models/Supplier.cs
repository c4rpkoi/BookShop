namespace Assignment.Models
{
    public class Supplier
    {
        public Supplier()
        {
            Book= new HashSet<Book>();
        }
        public Guid SupplierID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Book>? Book { get; set; }
    }
}
