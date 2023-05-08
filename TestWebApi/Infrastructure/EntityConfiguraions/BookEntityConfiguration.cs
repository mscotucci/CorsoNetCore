using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;
using TestWebApi.Entities;

namespace TestWebApi.Infrastructure.EntityConfiguraions;

public class BookEntityConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Description)
            .HasMaxLength(512);

        builder.Property(e => e.Genre)
            .HasMaxLength(256)
            .HasConversion<string>();

        builder.Property(e => e.Price).HasColumnType("decimal(18, 2)");

        builder.Property(e => e.PublishDate).HasColumnType("datetime");

        builder.Property(e => e.Title)
            .HasMaxLength(256);

        builder.HasOne(d => d.Author)
            .WithMany(p => p.Books)
            .HasForeignKey(d => d.AuthorId); ;
    }
}
