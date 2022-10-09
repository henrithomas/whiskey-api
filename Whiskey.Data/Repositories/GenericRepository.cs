using Whiskey.Data.Contracts;
using Whiskey.Data.Models;

namespace Whiskey.Data.Repositories;
public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : Spiritus 
{

}