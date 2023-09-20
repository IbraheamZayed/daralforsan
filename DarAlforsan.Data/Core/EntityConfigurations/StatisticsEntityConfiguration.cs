using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

namespace Core
{
    public class StatisticsEntityConfiguration : IEntityTypeConfiguration<Statistics>
    {
        public void Configure(EntityTypeBuilder<Statistics> builder)
        {
            builder.HasKey(a => a.ID);
            builder.Property(a => a.TitleAr).IsRequired().HasColumnType("nvarchar(100)");
            builder.Property(a => a.TitleEn).IsRequired().HasColumnType("nvarchar(100)");
            builder.Property(a => a.Img).IsRequired().HasColumnType("nvarchar(250)");
        }
    }
}