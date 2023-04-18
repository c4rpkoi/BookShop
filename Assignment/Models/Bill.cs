using Microsoft.AspNetCore.Identity;

namespace Assignment.Models
{
    public class Bill
    {
        public Guid BillID { get; set; }
        public  List<BillDetail>? BillLines { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AddressLine { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public decimal BillTotal { get; set; }
        public DateTime BillPlaced { get; set; }
        public int Status { get; set; }
        public string? UserId { get; set; }
        public IdentityUser? User { get; set; }

    }
}
