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
        var context = CreateContext();

        Assert.Pass();
    }

    [Test]
    public void CreateDatabase_UserTableNotEmpty()
    {
        var context = CreateContext();

        Assert.IsNotEmpty(context.Users);
    }

    private RecademyContext CreateContext()
    {
        var seeder = new DbContextSeeder();
        var context = new RecademyContext(new DbContextOptionsBuilder<RecademyContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .UseLazyLoadingProxies()
                .Options,
            seeder);

        context.Database.EnsureCreated();
        return context;
    }
}