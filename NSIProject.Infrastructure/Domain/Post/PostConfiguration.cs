using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace NSIProject.Infrastructure.Domain.Post;

public class PostConfiguration : IEntityTypeConfiguration<NSIProject.Domain.Entities.Post>
{
    public void Configure(EntityTypeBuilder<NSIProject.Domain.Entities.Post> builder)
    {
        builder.ToTable("Posts");
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).ValueGeneratedNever();
        builder.Property(e => e.Title).IsRequired().HasMaxLength(100);
        builder.Property(e => e.Content).IsRequired().HasMaxLength(500);
        builder.HasOne(e => e.User).WithMany(user => user.Posts).HasForeignKey("UserId").IsRequired();
    }
}