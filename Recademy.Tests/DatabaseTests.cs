using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Recademy.DataAccess;
using System;

namespace Recademy.Tests;

[TestFixture]
public class DatabaseTests
{
    [Test]
    public void CreateDatabase_EnsureCreated()
    {
        var context = new RecademyContext(new DbContextOptionsBuilder<RecademyContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .UseLazyLoadingProxies()
                .Options);

        context.Database.EnsureCreated();

        Assert.Pass();
    }
}