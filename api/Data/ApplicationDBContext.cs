using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions dBContextOptions) : base(dBContextOptions)
        {
        }

        public DbSet<Models.Stock> Stocks { get; set; } = null!;
        public DbSet<Models.Comment> Comments { get; set; } = null!;
    }
}