using Assignment.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace e_commerce_website.configurations
{
    public class cartdetailsconfig : IEntityTypeConfiguration<CartDetail>
    {
        public void Configure(EntityTypeBuilder<CartDetail> builder)
        {
            builder.HasKey(p => p.CartDetailID);
            //builder.hasone(p => p.cart).withmany(c => c.cartdetails).hasforeignkey(x => x.cartid).hasconstraintname("fk_gh");
            builder.Property(p => p.Amount).HasColumnType("int").IsRequired();
        }
    }
}
