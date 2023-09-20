using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

namespace Core
{
    public class SchoolFruitsEntityConfiguration : IEntityTypeConfiguration<SchoolFruits>
    {
        public void Configure(EntityTypeBuilder<SchoolFruits> builder)
        {
            builder.HasKey(a => a.ID);
            builder.Property(a => a.TitleAr).IsRequired().HasColumnType("nvarchar(100)");
            builder.Property(a => a.TitleEn).IsRequired().HasColumnType("nvarchar(250)");
            builder.Property(a => a.Img).IsRequired().HasColumnType("nvarchar(250)");
            builder.HasMany(a => a.FruitsDetails).WithOne(a => a.Fruits).HasForeignKey(a => a.FruitsID);
        }
    }
}