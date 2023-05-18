using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestWebApi.Entities;

namespace TestWebApi.Infrastructure.EntityConfiguraions
{
    public class RoleEntityConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).HasMaxLength(50).IsRequired();

            builder.HasMany(x => x.Users).WithMany(x => x.Roles);
        }
    }
}
