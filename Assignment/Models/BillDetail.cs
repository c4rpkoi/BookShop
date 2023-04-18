namespace Assignment.Models
{
    public class BillDetail
    {
        public Guid BillDetailID { get; set; }
        public Guid BillID { get; set; }
        public Guid BookID { get; set; }
        public int Amount { get; set; }
        public decimal Price { get; set; }
        public virtual Bill? Bill { get; set; }
        public virtual Book? Book { get; set; }
    }
}
