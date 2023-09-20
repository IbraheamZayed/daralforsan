using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

namespace Core
{
    public class SaidAboutUsEntityConfiguration : IEntityTypeConfiguration<SaidAboutUs>
    {
        public void Configure(EntityTypeBuilder<SaidAboutUs> builder)
        {
            builder.HasKey(a => a.ID);
            builder.Property(a => a.Name).IsRequired().HasColumnType("nvarchar(100)");
            builder.Property(a => a.MessageAr).IsRequired().HasColumnType("nvarchar(250)");
            builder.Property(a => a.MessageEn).IsRequired().HasColumnType("nvarchar(250)");
            builder.Property(a => a.Img).IsRequired().HasColumnType("nvarchar(250)");
        }
    }
}