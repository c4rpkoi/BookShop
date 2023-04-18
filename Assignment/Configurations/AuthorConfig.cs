using Assignment.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Assignment.Configurations
{
    public class AuthorConfig : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.ToTable("Author");//Đặt tên 
            builder.HasKey(p => p.AuthorID);//Khóa chính
            builder.Property(p => p.Name).HasColumnType("nvarchar(255)");
     

        }
    }
}
