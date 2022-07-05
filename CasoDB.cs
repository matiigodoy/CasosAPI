using Microsoft.EntityFrameworkCore;

namespace MinimalAPICasosIVR
{
    public class CasoDB : DbContext
    {
        public CasoDB(DbContextOptions<CasoDB> options)
            : base(options) { }
        public DbSet<Caso> Casos => Set<Caso>();
    }
}
