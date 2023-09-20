using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

namespace Core
{
    public class SchoolSystemEntityConfiguration : IEntityTypeConfiguration<SchoolSystem>
    {
        public void Configure(EntityTypeBuilder<SchoolSystem> builder)
        {
            builder.HasKey(a => a.ID);
            builder.Property(a => a.TitleAr).IsRequired().HasColumnType("nvarchar(100)");
            builder.Property(a => a.TitleEn).IsRequired().HasColumnType("nvarchar(100)");
            builder.Property(a => a.MessageAr).IsRequired().HasColumnType("nvarchar(250)");
            builder.Property(a => a.MessageEn).IsRequired().HasColumnType("nvarchar(250)");
        }
    }
}