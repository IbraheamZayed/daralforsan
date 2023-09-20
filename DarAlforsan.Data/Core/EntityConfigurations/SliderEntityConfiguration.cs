using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

namespace Core
{
    public class SliderEntityConfiguration : IEntityTypeConfiguration<Slider>
    {
        public void Configure(EntityTypeBuilder<Slider> builder)
        {
            builder.HasKey(a => a.ID);
            builder.Property(a => a.TitleAr).IsRequired().HasColumnType("nvarchar(100)");
            builder.Property(a => a.TitleEn).IsRequired().HasColumnType("nvarchar(100)");
            builder.Property(a => a.DetailsAr).IsRequired().HasColumnType("nvarchar(250)");
            builder.Property(a => a.DetailsEn).IsRequired().HasColumnType("nvarchar(250)");
            builder.Property(a => a.Img).IsRequired().HasColumnType("nvarchar(250)");
        }
    }
}