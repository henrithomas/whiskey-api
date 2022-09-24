using Microsoft.EntityFrameworkCore;
using Whiskey.Data.Models;

namespace Whiskey.Data.Context
{
    public class WhiskeyDb : DbContext
    {
        public WhiskeyDb(DbContextOptions<WhiskeyDb> options)
            : base(options) { }

        public DbSet<WhiskeyEntity> Whiskeys => Set<WhiskeyEntity>();
    }
}