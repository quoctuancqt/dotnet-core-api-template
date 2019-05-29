using Microsoft.EntityFrameworkCore;
using Demo.Persistence.ContextFactory;

namespace Demo.Persistence
{
    public class AppContextFactory : DesignTimeDbContextFactoryBase<ApplicationContext>
    {
        protected override ApplicationContext CreateNewInstance(DbContextOptions<ApplicationContext> options)
        {
            return new ApplicationContext(options);
        }
    }
}
