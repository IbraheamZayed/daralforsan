using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

namespace Core
{
    public class ResourceEntityConfiguration : IEntityTypeConfiguration<Resource>
    {
        public void Configure(EntityTypeBuilder<Resource> builder)
        {
            builder.HasKey(a => a.ResourceID);
            builder.Property(a => a.Name).HasColumnType("nvarchar(100)");
            builder.Property(a => a.LocalValue).HasColumnType("nvarchar(255)");
            builder.Property(a => a.LatinValue).HasColumnType("nvarchar(255)");
        }
    }
}