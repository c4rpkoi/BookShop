using Assignment.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Assignment.Configurations
{
    public class CategoryConfig : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(p => p.CategoryID);//Khóa chính
            builder.Property(p => p.Name).HasColumnType("nvarchar(255)");
            builder.Property(p => p.Description).HasColumnType("nvarchar(500)");
        }
    }
}
