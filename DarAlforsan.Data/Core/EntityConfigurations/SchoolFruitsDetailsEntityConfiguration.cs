using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

namespace Core
{
    public class SchoolFruitsDetailsEntityConfiguration : IEntityTypeConfiguration<SchoolFruitsDetails>
    {
        public void Configure(EntityTypeBuilder<SchoolFruitsDetails> builder)
        {
            builder.HasKey(a => a.ID);
            builder.Property(a => a.TitleAr).IsRequired().HasColumnType("nvarchar(250)");
            builder.Property(a => a.TitleEn).IsRequired().HasColumnType("nvarchar(250)");
        }
    }
}