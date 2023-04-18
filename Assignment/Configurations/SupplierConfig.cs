using Assignment.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Assignment.Configurations
{
    public class SupplierConfig : IEntityTypeConfiguration<Supplier>
    {
        public void Configure(EntityTypeBuilder<Supplier> builder)
        {
            builder.HasKey(p => p.SupplierID);//Khóa chính
            builder.Property(p => p.Name).HasColumnType("nvarchar(255)");
            builder.Property(p => p.Description).HasColumnType("nvarchar(500)");

        }
    }
}
