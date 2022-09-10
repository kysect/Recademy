using Microsoft.EntityFrameworkCore;

namespace Recademy.DataAccess.Seeding;

public interface IEntitySeedingGenerator
{
    void Seed(ModelBuilder modelBuilder);
}