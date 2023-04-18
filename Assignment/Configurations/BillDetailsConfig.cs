using Assignment.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace e_commerce_website.Configurations
{
    public class BillDetailsConfig : IEntityTypeConfiguration<BillDetail>
    {
        public void Configure(EntityTypeBuilder<BillDetail> builder)
        {
            builder.ToTable("BillDetails");
            builder.HasKey(p => p.BillDetailID);
            builder.Property(p=>p.Amount).HasColumnType("int");
            builder.Property(p => p.Price).HasColumnType("decimal");
            builder.HasOne(p => p.Bill).WithMany(x => x.BillLines).HasForeignKey(x => x.BillID).HasConstraintName("FK_HD");
            


        }
    }
}
