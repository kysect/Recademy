using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Recademy.DataAccess;
using Recademy.DataAccess.Seeding;
using System;

namespace Recademy.Tests;

[TestFixture]
public class DatabaseTests
{
    [Test]
    public void CreateDatabase_EnsureCreated()
    {
        var seeder = new DbContextSeeder();
        var context = new RecademyContext(new DbContextOptionsBuilder<RecademyContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .UseLazyLoadingProxies()
                .Options,
            seeder);

        context.Database.EnsureCreated();

        Assert.Pass();
    }
}