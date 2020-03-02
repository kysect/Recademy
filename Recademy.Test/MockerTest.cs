using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Recademy.Api;
using Recademy.Library.Models;
using Recademy.Mock;
using Recademy.Test.Tools;

namespace Recademy.Test
{
    public class MockerTest
    {
        private RecademyContext _context;

        [SetUp]
        public void Setup()
        {
            _context = TestDatabaseProvider.GetDatabaseContext();
            var mocker = new Mocker(_context);
            mocker.Mock();
        }

        [Test]
        public void MockDatabase_AtLeastOneUserExist()
        {
            List<User> users = _context.Users.ToList();

            Assert.IsTrue(users.Count > 0);
        }
    }
}