using Microsoft.EntityFrameworkCore;
using WhiskeyApi.Models;

namespace WhiskeyApi.DataContext
{
    class WhiskeyDb : DbContext
    {
        public WhiskeyDb(DbContextOptions<WhiskeyDb> options)
            : base(options) { }

        public DbSet<Whiskey> Whiskeys => Set<Whiskey>();
    }
}