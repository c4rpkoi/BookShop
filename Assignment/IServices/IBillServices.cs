using Assignment.Models;

namespace Assignment.IServices
{
    public interface IBillServices
    {
        Task CreateBillAsync(Bill bill);
        Task ChangeBillStatus(Guid id);
    }
}
