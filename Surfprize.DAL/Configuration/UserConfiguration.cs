using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Surfprize.Entity;

namespace Surfprize.DAL.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(k => k.UserId);
            builder.Property(p => p.Email)
                .IsRequired();
            builder.Property(p => p.FirstName)
                .IsRequired(false)
                .HasMaxLength(250);
            builder.Property(p => p.LastName)
                .IsRequired(false)
                .HasMaxLength(250);
            builder.Property(p => p.Password)
                .IsRequired(false)
                .HasMaxLength(250);
            builder.Property(p => p.PhoneNumber)
                .IsRequired(false)
                .HasMaxLength(250);
            builder.Property(p => p.Role)
                .IsRequired();

            builder.Property(p => p.CreatedDate)
                .IsRequired();
            builder.Property(p => p.IsDeleted)
                .IsRequired()
                .HasDefaultValue(false);
            builder.Property(p => p.UpdateDate)
                .IsRequired();
        }
    }
}
