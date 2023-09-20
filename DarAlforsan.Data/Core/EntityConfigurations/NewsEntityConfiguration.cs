using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

namespace Core
{
    public class NewsEntityConfiguration : IEntityTypeConfiguration<News>
    {
        public void Configure(EntityTypeBuilder<News> builder)
        {
            builder.HasKey(a => a.ID);
            builder.Property(a => a.TitleAr).IsRequired().HasColumnType("nvarchar(100)");
            builder.Property(a => a.TitleEn).IsRequired().HasColumnType("nvarchar(100)");
            builder.Property(a => a.DetailsAr).IsRequired().HasColumnType("nvarchar(250)");
            builder.Property(a => a.DetailsEn).IsRequired().HasColumnType("nvarchar(250)");
            builder.Property(a => a.MainImg).IsRequired().HasColumnType("nvarchar(250)");
            builder.Property(a => a.Imgs).IsRequired().HasColumnType("nvarchar(max)");
        }
    }
}