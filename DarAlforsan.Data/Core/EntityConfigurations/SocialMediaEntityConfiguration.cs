using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

namespace Core
{
    public class SocialMediaEntityConfiguration : IEntityTypeConfiguration<SocialMedia>
    {
        public void Configure(EntityTypeBuilder<SocialMedia> builder)
        {
            builder.HasKey(a => a.ID);
            builder.Property(a => a.Name).IsRequired().HasColumnType("nvarchar(100)");
            builder.Property(a => a.Img).IsRequired().HasColumnType("nvarchar(250)");
            builder.Property(a => a.Url).IsRequired().HasColumnType("nvarchar(100)");
        }
    }
}