using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestTaskValetax.Domain.Models;

namespace TestTaskValetax.Infrastructure.Configurations;
public class NodeConfiguration : IEntityTypeConfiguration<Node>
{
    public void Configure(EntityTypeBuilder<Node> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id");

        builder.Property(x => x.ParentId)
            .HasColumnName("parent_id")
            .IsRequired(false);

        builder.HasOne<Node>()
            .WithMany(x => x.Children)
            .HasForeignKey(x => x.ParentId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired(false);

        builder.Property(x => x.Name)
            .HasColumnName("name")
            .IsRequired();

        builder.Property(x => x.Path)
            .HasColumnName("path")
            .HasColumnType("ltree")
            .IsRequired();

        builder.HasIndex(x => new { x.ParentId, x.Name })
               .IsUnique()
               .HasDatabaseName("IX_Node_ParentId_Name");
    }
}
