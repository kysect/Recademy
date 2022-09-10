using Microsoft.EntityFrameworkCore;

namespace Recademy.DataAccess.Seeding;

public interface IDbContextSeeder
{
    void Seed(ModelBuilder modelBuilder);
}