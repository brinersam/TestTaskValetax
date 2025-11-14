using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestTaskValetax.Core.Efcore;
using TestTaskValetax.Domain.Models;

namespace TestTaskValetax.Infrastructure.Configurations;
public class JournalInfoConfiguration : IEntityTypeConfiguration<JournalInfo>
{
    public void Configure(EntityTypeBuilder<JournalInfo> builder)
    {
        builder.HasKey(x => x.EventId);

        builder.Property(x => x.EventId)
            .HasColumnType("BIGINT")
            .HasColumnName("event_id")
            .ValueGeneratedOnAdd();

        builder.Property(x => x.CreatedAt)
            .HasColumnName("created_at")
            .HasDefaultValueSql("NOW()");

        builder.Property(x => x.Context)
            .HasColumnName("context")
            .JsonVOConverter();

        builder.Property(x => x.Trace)
            .HasColumnName("trace");
    }
}
