using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Recademy.DataAccess.Seeding;

public class DbContextSeeder : IDbContextSeeder
{
    private readonly List<IEntitySeedingGenerator> _generators = new List<IEntitySeedingGenerator>();

    public DbContextSeeder()
    {
        const int userCount = 10;

        _generators.Add(new UserSeedingGenerator(userCount));
    }
    public void Seed(ModelBuilder modelBuilder)
    {
        foreach (IEntitySeedingGenerator entityGenerator in _generators)
        {
            entityGenerator.Seed(modelBuilder);
        }
    }
}