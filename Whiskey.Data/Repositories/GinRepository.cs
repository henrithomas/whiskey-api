using Microsoft.EntityFrameworkCore;
using Whiskey.Data.Contracts;
using Whiskey.Data.Repositories;
using Whiskey.Data.Models;
using Whiskey.Data.Context;

namespace Whiskey.Data.Repositories;
public class GinRepository : GenericRepository<Gin>, IGinRepository
{
    public GinRepository(WhiskeyDb db) : base(db) { }
}