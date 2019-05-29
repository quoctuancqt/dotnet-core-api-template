using Demo.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Demo.Persistence.Configurations
{
    public class ConfigurateProduct : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");
            builder.Property(p => p.Name).IsRequired();
            builder.Property(p => p.Price).HasColumnType("money");
        }
    }
}
