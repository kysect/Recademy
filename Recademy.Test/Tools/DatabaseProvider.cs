using System;
using Microsoft.EntityFrameworkCore;
using Recademy.Api;

namespace Recademy.Test.Tools
{
    public static class DatabaseProvider
    {
        private static RecademyContext GetDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<RecademyContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new RecademyContext(options);
            databaseContext.Database.EnsureCreated();

            //TODO: use mocker

            return databaseContext;
        }
    }
}
