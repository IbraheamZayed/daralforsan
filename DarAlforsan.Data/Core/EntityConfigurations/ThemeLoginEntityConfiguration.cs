using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

namespace Core
{
    public class ThemeLoginEntityConfiguration : IEntityTypeConfiguration<ThemeLogin>
    {
        public void Configure(EntityTypeBuilder<ThemeLogin> builder)
        {
            builder.HasKey(a => a.ThemeID);
            builder.Property(a => a.ThemeName).IsRequired().HasColumnType("nvarchar(255)");
            builder.Property(a => a.BGImgPath).IsRequired().HasColumnType("nvarchar(255)");
            builder.Property(a => a.LogoImgPath).IsRequired().HasColumnType("nvarchar(255)");
            builder.Property(a => a.SideImgPath).IsRequired().HasColumnType("nvarchar(255)");
            builder.Property(a => a.SideImgPadding).IsRequired().HasColumnType("nvarchar(255)");
            builder.Property(a => a.LogoImgPadding).IsRequired().HasColumnType("nvarchar(255)");
            builder.Property(a => a.ForeColor).IsRequired().HasColumnType("nvarchar(255)");
            builder.Property(a => a.FromDate).HasColumnType("datetime");
            builder.Property(a => a.ToDate).HasColumnType("datetime");
        }
    }
}