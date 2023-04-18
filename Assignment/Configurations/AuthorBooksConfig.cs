using Assignment.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Assignment.Configurations
{
    public class AuthorBooksConfig : IEntityTypeConfiguration<AuthorBooks>
    {
        public void Configure(EntityTypeBuilder<AuthorBooks> builder)
        {
            builder.HasKey(p => p.Id);//Khóa chính
            builder.HasOne(p => p.Author).WithMany(x => x.AuthorBooks).HasForeignKey(k => k.AuthorID)
              .HasConstraintName("FK_KHkj");//Khóa p
            builder.HasOne(p => p.Books).WithMany(x => x.AuthorBooks).HasForeignKey(k => k.BookID)
              .HasConstraintName("FK_Khuy");//Khóa p
        }
    }
}
