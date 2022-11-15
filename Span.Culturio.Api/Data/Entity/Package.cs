using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Span.Culturio.Api.Models;

namespace Span.Culturio.Api.Data.Entity
{
    public class Package
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int[] CultureObjectIds { get; set; }
        public int ValidDays { get; set; }
    }

    public class PackageConfigurationBuilder : IEntityTypeConfiguration<Package>
    {
        public void Configure(EntityTypeBuilder<Package> builder)
        {
            var arrayConverter = new ValueConverter<int[], string>(
                v => string.Join(";", v),
                v => v.Split(";", StringSplitOptions.RemoveEmptyEntries)
                      .Select(num => int.Parse(num))
                      .ToArray()
            );

            builder.ToTable(nameof(Package));
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name)
                .IsRequired();
            builder.Property(p => p.CultureObjectIds)
                .IsRequired()
                .HasConversion(arrayConverter);
            builder.Property(p => p.ValidDays)
                .IsRequired();
        }
    }
}
