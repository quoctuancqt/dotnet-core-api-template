using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class AppContextFactory : ContextFactory.DesignTimeDbContextFactoryBase<ApplicationContext>
    {
        protected override ApplicationContext CreateNewInstance(DbContextOptions<ApplicationContext> options)
        {
            return new ApplicationContext(options);
        }
    }
}
