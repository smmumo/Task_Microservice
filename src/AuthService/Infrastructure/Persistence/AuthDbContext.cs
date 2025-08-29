

using System.Reflection;
using AuthService.Domain.Repository;
using Microsoft.EntityFrameworkCore;

using Microsoft.EntityFrameworkCore.Storage;

namespace AuthService.Infrastructure.Persistence;

/// <summary>
/// Represents the applications database context.
/// </summary>
public sealed class AuthDbContext : DbContext, IUnitOfWork
{
    public AuthDbContext(DbContextOptions options)
        : base(options)
    {
    }

    public Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
          => Database.BeginTransactionAsync(cancellationToken);    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {        
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());       
        base.OnModelCreating(modelBuilder);
    }

   
}