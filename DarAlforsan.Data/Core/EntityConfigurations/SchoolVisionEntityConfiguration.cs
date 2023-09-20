using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

namespace Core
{
    public class SchoolVisionEntityConfiguration : IEntityTypeConfiguration<SchoolVision>
    {
        public void Configure(EntityTypeBuilder<SchoolVision> builder)
        {
            builder.HasKey(a => a.ID);
            builder.Property(a => a.TitleAr).IsRequired().HasColumnType("nvarchar(100)");
            builder.Property(a => a.TitleEn).IsRequired().HasColumnType("nvarchar(100)");
            builder.Property(a => a.MessageAr).IsRequired().HasColumnType("nvarchar(250)");
            builder.Property(a => a.MessageEn).IsRequired().HasColumnType("nvarchar(250)");
        }
    }
}