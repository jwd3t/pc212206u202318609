using Microsoft.EntityFrameworkCore;
using PC212206.U202318609.Capbase.Platform.Paperwork.Domain.Model.Aggregates;

namespace PC212206.U202318609.Capbase.Platform.Paperwork.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

/// <summary>
///     Contains Entity Framework Core mappings for the Paperwork bounded context.
/// </summary>
public static class ModelBuilderExtensions
{
    /// <summary>
    ///     Applies the Paperwork persistence model.
    /// </summary>
    /// <param name="builder">The model builder.</param>
    public static void ApplyPaperworkConfiguration(this ModelBuilder builder)
    {
        builder.Entity<Covenant>(entity =>
        {
            entity.HasKey(c => c.Id);
            entity.Property(c => c.Id).IsRequired().ValueGeneratedOnAdd();
            entity.Property(c => c.Status).HasConversion<string>().IsRequired().HasMaxLength(20);
            entity.Property(c => c.Footnotes).HasMaxLength(500);
            entity.Property(c => c.CreatedAt);
            entity.Property(c => c.UpdatedAt);

            entity.OwnsOne(c => c.DocumentIdentifier, owned =>
            {
                owned.WithOwner().HasForeignKey("CovenantId");
                owned.Property<int>("CovenantId").HasColumnName("id");

                owned.Property(p => p.Value)
                    .HasColumnName("document_identifier")
                    .IsRequired();

                owned.HasIndex(p => p.Value).IsUnique();
            });

            entity.OwnsOne(c => c.ClientId, owned =>
            {
                owned.WithOwner().HasForeignKey("CovenantId");
                owned.Property<int>("CovenantId").HasColumnName("id");

                owned.Property(p => p.Value)
                    .HasColumnName("client_id")
                    .IsRequired();
            });

            entity.OwnsOne(c => c.Period, owned =>
            {
                owned.WithOwner().HasForeignKey("CovenantId");
                owned.Property<int>("CovenantId").HasColumnName("id");

                owned.Property(p => p.StartDate)
                    .HasColumnName("period_start_date")
                    .IsRequired();

                owned.Property(p => p.EndDate)
                    .HasColumnName("period_end_date")
                    .IsRequired();
            });

            entity.OwnsOne(c => c.TotalValue, owned =>
            {
                owned.WithOwner().HasForeignKey("CovenantId");
                owned.Property<int>("CovenantId").HasColumnName("id");

                owned.Property(p => p.Value)
                    .HasColumnName("monetary_amount_value")
                    .HasColumnType("decimal(18,2)")
                    .IsRequired();

                owned.Property(p => p.Currency)
                    .HasColumnName("monetary_amount_currency")
                    .HasMaxLength(12)
                    .IsRequired();
            });

            entity.Navigation(c => c.DocumentIdentifier).IsRequired();
            entity.Navigation(c => c.ClientId).IsRequired();
            entity.Navigation(c => c.Period).IsRequired();
            entity.Navigation(c => c.TotalValue).IsRequired();
        });
    }
}
