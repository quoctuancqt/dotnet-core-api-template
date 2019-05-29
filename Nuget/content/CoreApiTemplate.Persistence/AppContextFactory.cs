using Microsoft.EntityFrameworkCore;
using CoreApiTemplate.Persistence.ContextFactory;

namespace CoreApiTemplate.Persistence
{
    public class AppContextFactory : DesignTimeDbContextFactoryBase<ApplicationContext>
    {
        protected override ApplicationContext CreateNewInstance(DbContextOptions<ApplicationContext> options)
        {
            return new ApplicationContext(options);
        }
    }
}
