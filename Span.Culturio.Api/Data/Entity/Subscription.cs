using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Span.Culturio.Api.Enum;

namespace Span.Culturio.Api.Data.Entity
{
    public class Subscription
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int PackageId { get; set; }
        public string Name { get; set; }
        public string? ActiveFrom { get; set; }
        public string? ActiveTo { get; set;}
        public int RecordedVisits { get; set; }

        public bool isActive()
        {
            if(ActiveFrom is null || ActiveTo is null) return false;

            var now = DateTime.Now;
            var startDate = DateTime.Parse(ActiveFrom);
            var endDate = DateTime.Parse(ActiveTo);

            if (DateTime.Compare(now, startDate) >= 0)
            {
                if (DateTime.Compare(now, endDate) <= 0)
                {
                    return true;
                }
            }

            return false;
        }
    }

    public class SubscriptionConfigurationBuilder : IEntityTypeConfiguration<Subscription>
    {
        public void Configure(EntityTypeBuilder<Subscription> builder)
        {
            builder.ToTable(nameof(Subscription));
            builder.HasKey(t => t.Id);
            builder.Property(t => t.UserId)
                .IsRequired();
            builder.Property(t => t.PackageId)
                .IsRequired();
            builder.Property(t => t.Name)
                .IsRequired();
            builder.Property(t => t.ActiveFrom);
            builder.Property(t => t.ActiveTo);
            builder.Property(t => t.RecordedVisits)
                .IsRequired()
                .HasDefaultValue(0);
        }
    }
}
