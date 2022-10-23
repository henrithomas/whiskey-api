using Microsoft.EntityFrameworkCore;
using Whiskey.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Whiskey.Data.Context;
public class WhiskeyDb : DbContext
{
    public WhiskeyDb(DbContextOptions<WhiskeyDb> options)
        : base(options) { }

    public DbSet<WhiskeyEntity> Whiskeys => Set<WhiskeyEntity>();
    public DbSet<Gin> Gins => Set<Gin>();
}

public class WhiskeyDbFactory : IDesignTimeDbContextFactory<WhiskeyDb>
{
    public WhiskeyDb CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<WhiskeyDb>();
        optionsBuilder.UseSqlite("Data Source=C:\\Users\\mt200\\Desktop\\projects\\whiskey-api\\db\\Whiskeys.db");
        return new WhiskeyDb(optionsBuilder.Options);
    }
}