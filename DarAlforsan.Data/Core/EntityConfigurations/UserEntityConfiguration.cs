using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

namespace Core
{
    public class UserEntityConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(a => a.UserID);
            builder.Property(a => a.LocalName).IsRequired().HasColumnType("nvarchar(100)");
            builder.Property(a => a.LatinName).HasColumnType("nvarchar(100)");
            builder.Property(a => a.UserName).IsRequired().HasColumnType("nvarchar(50)");
            builder.Property(a => a.Password).IsRequired().HasColumnType("nvarchar(50)");
            builder.Property(a => a.Email).HasColumnType("nvarchar(100)");
            builder.Property(a => a.MobileNo).HasColumnType("nvarchar(20)");
            builder.Property(a => a.PhotoName).HasColumnType("nvarchar(100)");
            builder.Property(a => a.Id).IsRequired().HasColumnType("nvarchar(20)");
            builder.Property(a => a.DeviceID).HasColumnType("nvarchar(255)");
            builder.Property(a => a.AddDate).IsRequired().HasColumnType("datetime");
            builder.Property(a => a.LastLoginDate).HasColumnType("datetime");
            builder.Property(a => a.Theme).HasColumnType("nvarchar(20)");
        }
    }
}