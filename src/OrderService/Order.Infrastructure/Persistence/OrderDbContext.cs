namespace Infrastructure.Persistence;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Order.Application.Data;
using Order.Domain.Core;
using Order.Domain.Entity;


/// <summary>
/// Represents the applications database context.
/// </summary>
public sealed class OrderDbContext : DbContext, IUnitOfWork, IDbContext
{
    public OrderDbContext(DbContextOptions options)
        : base(options)
    {
    }
    
    protected override void OnConfiguring(
    	DbContextOptionsBuilder optionsBuilder) 
	{ 
    	optionsBuilder.UseInMemoryDatabase("OrderDb"); 
	} 
    
     public Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
         => Database.BeginTransactionAsync(cancellationToken);

   
   public new DbSet<TEntity> Set<TEntity>()
            where TEntity : BaseEntity
            => base.Set<TEntity>();

    // public Task<int> ExecuteSqlAsync(string sql, IEnumerable<SqlParameter> parameters, CancellationToken cancellationToken = default)
    // {
    //    return Database.ExecuteSqlRawAsync(sql, parameters, cancellationToken);
    // }

    // public async Task<TEntity> GetBydIdAsync<TEntity>(Guid id) where TEntity : BaseEntity
    // {
    //    return await Set<TEntity>().FirstOrDefaultAsync(e => e.Id == id)
    // }

    public void Insert<TEntity>(TEntity entity) where TEntity : BaseEntity
    {
        Set<TEntity>().Add(entity);
    }

    public void InsertRange<TEntity>(IReadOnlyCollection<TEntity> entities) where TEntity : BaseEntity
    {
         Set<TEntity>().AddRange(entities);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // modelBuilder.Entity<BaseEntity>()
        // .HasNoKey();
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());  
       
        //modelBuilder.ApplyUtcDateTimeConverter();
        base.OnModelCreating(modelBuilder);
    }

    void IDbContext.Remove<TEntity>(TEntity entity)
    {
        Set<TEntity>().Remove(entity);
    }

    Task<IDbContextTransaction> IUnitOfWork.BeginTransactionAsync(CancellationToken cancellationToken)
    {
        return Database.BeginTransactionAsync(cancellationToken);
    }
}