using Microsoft.EntityFrameworkCore;
using Persistence.ContextFactory;

namespace Persistence
{
    public class AppContextFactory : DesignTimeDbContextFactoryBase<ApplicationContext>
    {
        protected override ApplicationContext CreateNewInstance(DbContextOptions<ApplicationContext> options)
        {
            return new ApplicationContext(options);
        }
    }
}
