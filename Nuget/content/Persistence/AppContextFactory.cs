using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class AppContextFactory : ContextFactory.DesignTimeDbContextFactoryBase<AppContext>
    {
        protected override AppContext CreateNewInstance(DbContextOptions<AppContext> options)
        {
            return new AppContext(options);
        }
    }
}
