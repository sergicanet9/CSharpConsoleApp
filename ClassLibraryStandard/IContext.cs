using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

public interface IContext : IDisposable
{
    int SaveChanges();

    EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
    DbSet<TEntity> Set<TEntity>() where TEntity : class;
}