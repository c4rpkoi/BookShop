using Assignment.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace e_commerce_website.Configurations
{
    public class BooksConfig : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.ToTable("Books");
            builder.HasKey(p => p.ID);
            builder.Property(p=>p.Name).HasColumnType("nvarchar(100)");
            builder.Property(p => p.Price).HasColumnType("decimal");
            builder.Property(p => p.Description).HasColumnType("nvarchar(1000)");
            builder.Property(p => p.ImageUrl).HasColumnType("nvarchar(1000)");
            builder.Property(p => p.AvailableQuantity).HasColumnType("int");
           
            
        
            builder.HasOne(p => p.Category).WithMany(c => c.Books).HasForeignKey(p => p.CategoryID).HasConstraintName("FK_type");
            builder.HasOne(p => p.Supplier).WithMany(c => c.Book).HasForeignKey(p => p.SupplierID).HasConstraintName("FK_sup");


        }
    }
}
