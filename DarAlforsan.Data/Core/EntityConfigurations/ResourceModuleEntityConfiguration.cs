using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

namespace Core
{
    public class ResourceModuleEntityConfiguration : IEntityTypeConfiguration<ResourceModule>
    {
        public void Configure(EntityTypeBuilder<ResourceModule> builder)
        {
            builder.HasKey(a => a.ModuleID);
            builder.Property(a => a.Name).HasColumnType("nvarchar(100)");
            builder.Property(a => a.LocalName).HasColumnType("nvarchar(255)");
            builder.Property(a => a.LatinName).HasColumnType("nvarchar(255)");
            builder.HasMany(a => a.Resources).WithOne(a => a.ResourceModule).HasForeignKey(a => a.ModuleID);
        }
    }
}