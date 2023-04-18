namespace Assignment.Models
{
    public class CartDetail
    {
        public Guid CartDetailID { get; set; }
        public Guid? CartID { get; set; }
        public Guid BookID { get; set; }
        public int Amount { get; set; }
        public  Cart? Cart { get; set; }
        public  Book? Books { get; set; }
        
    }
}
