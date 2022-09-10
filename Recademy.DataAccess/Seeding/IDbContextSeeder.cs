using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Recademy.DataAccess.Seeding;

public interface IEntityGenerator
{
    void Seed(ModelBuilder modelBuilder);
}

public interface IDbContextSeeder
{
    void Seed(ModelBuilder modelBuilder);
}

public class DbContextSeeder : IDbContextSeeder
{
    private readonly List<IEntityGenerator> _generators = new List<IEntityGenerator>();

    public DbContextSeeder()
    {
        
    }

    private T Register<T>(T entityGenerator) where T : IEntityGenerator
    {
        _generators.Add(entityGenerator);
        return entityGenerator;
    }

    public void Seed(ModelBuilder modelBuilder)
    {
        foreach (IEntityGenerator entityGenerator in _generators)
        {
            entityGenerator.Seed(modelBuilder);
        }
    }
}