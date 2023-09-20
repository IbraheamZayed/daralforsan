using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

namespace Core
{
    public class ContactUsEntityConfiguration : IEntityTypeConfiguration<ContactUs>
    {
        public void Configure(EntityTypeBuilder<ContactUs> builder)
        {
            builder.HasKey(a => a.ID);
            builder.Property(a => a.Name).IsRequired().HasColumnType("nvarchar(100)");
            builder.Property(a => a.Email).IsRequired().HasColumnType("nvarchar(100)");
            builder.Property(a => a.MobileNo).IsRequired().HasColumnType("nvarchar(100)");
            builder.Property(a => a.Message).IsRequired().HasColumnType("nvarchar(250)");
        }
    }
}