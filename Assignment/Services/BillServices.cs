using Assignment.Data;
using Assignment.IServices;
using Assignment.Models;

namespace Assignment.Services
{
    public class BillServices : IBillServices
    {
        private readonly ShopContext _shopContext;
        private readonly Cart _cart;

        public BillServices(ShopContext shopContext, Cart cart)
        {
            _shopContext = shopContext;
            _cart = cart;
        }

        public async Task ChangeBillStatus(Guid id)
        {
            var bill = _shopContext.Bills.FirstOrDefault(p => p.BillID == id);

            bill.Status = 1;
            _shopContext.Bills.Update(bill);

            await _shopContext.SaveChangesAsync();
        }

        public async Task CreateBillAsync(Bill bill)
        {
            bill.BillPlaced = DateTime.Now;
            decimal totalPrice = 0M;

            var cartDetails = _cart.CartDetails;

            foreach (var cartDetail in cartDetails)
            {
                var billDetail = new BillDetail()
                {
                    Amount = cartDetail.Amount,
                    BookID = cartDetail.BookID,
                    Bill = bill,
                    Price = cartDetail.Books.Price,
                };
                var book = _shopContext.Books.FirstOrDefault(c => c.ID == cartDetail.BookID);
                book.AvailableQuantity = book.AvailableQuantity - cartDetail.Amount;
                _shopContext.Books.Update(book);
                totalPrice += billDetail.Price * billDetail.Amount;
                _shopContext.BillDetails.Add(billDetail);
            }

            bill.BillTotal = totalPrice;
            bill.Status = 0;
            _shopContext.Bills.Add(bill);

            await _shopContext.SaveChangesAsync();
        }
    }
}
