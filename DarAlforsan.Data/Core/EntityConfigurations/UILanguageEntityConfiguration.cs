using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

namespace Core
{
    public class UILanguageEntityConfiguration : IEntityTypeConfiguration<UILanguage>
    {
        public void Configure(EntityTypeBuilder<UILanguage> builder)
        {
            builder.HasKey(a => a.LanguageID);
            builder.Property(a => a.Name).IsRequired().HasColumnType("nvarchar(50)");
            builder.Property(a => a.LocalName).HasColumnType("nvarchar(100)");
            builder.Property(a => a.LatinName).HasColumnType("nvarchar(100)");
            builder.Property(a => a.Direction).IsRequired().HasColumnType("nvarchar(20)");
            builder.Property(a => a.ISOLanguageName).HasColumnType("nvarchar(4)");
            builder.HasMany(a => a.Users).WithOne(a => a.UILanguage).HasForeignKey(a => a.UILanguageID);
        }
    }
}