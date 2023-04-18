using Assignment.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace e_commerce_website.Configurations
{
    public class BillConfig : IEntityTypeConfiguration<Bill>
    {
        public void Configure(EntityTypeBuilder<Bill> builder)
        {
            builder.ToTable("Bill");//Đặt tên 
            builder.HasKey(p => p.BillID);//Khóa chính
            builder.Property(p => p.FirstName).HasColumnType("nvarchar(500)");
            builder.Property(p => p.LastName).HasColumnType("nvarchar(500)");
            builder.Property(p => p.AddressLine).HasColumnType("nvarchar(500)");
            builder.Property(p => p.PhoneNumber).HasColumnType("nvarchar(500)");
            builder.Property(p => p.Email).HasColumnType("nvarchar(500)");
            builder.Property(p => p.BillPlaced).HasColumnType("DateTime");
            //builder.HasOne(p => p.User).WithMany(x => x.Bill).HasForeignKey(k => k.UserID)
              //  .HasConstraintName("FK_KH");//Khóa phụ
            //builder.HasOne(p => p.Staff).WithMany(x => x.Bill).HasForeignKey(k => k.StaffID)
            //    .HasConstraintName("FK_NV");//Khóa phụ
            builder.Property(p => p.BillTotal).HasColumnType("decimal");
            builder.Property(p => p.Status).HasColumnType("int");
        }
    }
}
