using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

namespace Core
{
    public class BranchEntityConfiguration : IEntityTypeConfiguration<Branch>
    {
        public void Configure(EntityTypeBuilder<Branch> builder)
        {
            builder.HasKey(a => a.BranchID);
            builder.Property(a => a.LocalName).IsRequired().HasColumnType("nvarchar(100)");
            builder.Property(a => a.LatinName).IsRequired().HasColumnType("nvarchar(100)");
            builder.HasMany(a => a.Users).WithOne(a => a.Branch).HasForeignKey(a => a.BranchID);
        }
    }
}