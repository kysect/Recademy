﻿using System;
using Microsoft.EntityFrameworkCore;
using Recademy.Api;

namespace Recademy.Test.Tools
{
    public static class TestDatabaseProvider
    {
        public static RecademyContext GetDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<RecademyContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new RecademyContext(options);
            databaseContext.Database.EnsureCreated();

            //TODO: Move all generation logic to mocker. Split for tow method - with empty db and with generated data

            return databaseContext;
        }
    }
}
