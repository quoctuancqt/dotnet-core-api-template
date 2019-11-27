using CoreApiTemplate.Domain.Identities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreApiTemplate.Persistence.Configurations
{
    public class ConfigurateApplicationUser : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(au => au.PasswordHash)
               .IsRequired();

            builder.Property(au => au.UserName)
                .IsRequired();
        }
    }
}
