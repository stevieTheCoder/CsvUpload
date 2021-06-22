using Microsoft.EntityFrameworkCore;
using Venture.SharedKernel.Infrastructure;

namespace CsvUpload.Infrastructure
{
    public class ContextFactory : DesignTimeDbContextFactoryBase<ApplicationContext>
    {
        protected override ApplicationContext CreateNewInstance(DbContextOptions<ApplicationContext> options)
        {
            return new ApplicationContext(options);
        }
    }
}
