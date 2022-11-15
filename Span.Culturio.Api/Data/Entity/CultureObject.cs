using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Span.Culturio.Api.Data.Entity
{
    public class CultureObject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ContactEmail { get; set; }
        public string Address { get; set; }
        public int ZipCode { get; set; }
        public string City { get; set; }
        public int AdminUserId { get; set; }

        public virtual User AdminUser { get; set; }
    }

    public class CultureObjectConfigurationBuilder : IEntityTypeConfiguration<CultureObject>
    {
        public void Configure(EntityTypeBuilder<CultureObject> builder)
        {
            builder.ToTable(nameof(CultureObject));
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name)
                .IsRequired();
            builder.Property(x => x.ContactEmail)
                .IsRequired();
            builder.Property(x => x.Address)
                .IsRequired();
            builder.Property(x => x.ZipCode)
                .IsRequired();
            builder.Property(x => x.City)
                .IsRequired();
            builder.Property(x => x.AdminUserId)
                .IsRequired();
            builder.HasOne(x => x.AdminUser)
                .WithMany(u => u.CultureObjects)
                .HasForeignKey(x => x.AdminUserId);
        }
    }
}
