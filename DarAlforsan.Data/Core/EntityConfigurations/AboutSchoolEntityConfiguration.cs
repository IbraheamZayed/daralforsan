using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

namespace Core
{
    public class AboutSchoolEntityConfiguration : IEntityTypeConfiguration<AboutSchool>
    {
        public void Configure(EntityTypeBuilder<AboutSchool> builder)
        {
            builder.HasKey(a => a.ID);
            builder.Property(a => a.AddressAr).IsRequired().HasColumnType("nvarchar(100)");
            builder.Property(a => a.AddressEn).IsRequired().HasColumnType("nvarchar(100)");
            builder.Property(a => a.MobileNo).IsRequired().HasColumnType("nvarchar(100)");
            builder.Property(a => a.WhatsAppNo).IsRequired().HasColumnType("nvarchar(100)");
            builder.Property(a => a.Email).IsRequired().HasColumnType("nvarchar(100)");
            builder.Property(a => a.DetailsAr).IsRequired().HasColumnType("nvarchar(500)");
            builder.Property(a => a.DetailsEn).IsRequired().HasColumnType("nvarchar(500)");
            builder.Property(a => a.Logo).IsRequired().HasColumnType("nvarchar(100)");
            builder.Property(a => a.RegisterationUrl).HasColumnType("nvarchar(100)");
        }
    }
}