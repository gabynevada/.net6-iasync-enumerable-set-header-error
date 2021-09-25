using Microsoft.EntityFrameworkCore;

namespace SetResponseHeaders.Db
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }
        
        public virtual DbSet<Data> Data { get; set; }
    }
}