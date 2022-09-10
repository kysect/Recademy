using Bogus;
using Microsoft.EntityFrameworkCore;
using Recademy.Core.Models.Users;
using Recademy.Core.Types;
using System;
using System.Collections.Generic;

namespace Recademy.DataAccess.Seeding;

public class UserSeedingGenerator : IEntitySeedingGenerator
{
    private readonly List<User> _users;
    private readonly List<RecademyUser> _recademyUsers;

    public UserSeedingGenerator(int count)
    {
        var faker = new Faker("ru");

        _users = new List<User>();
        _recademyUsers = new List<RecademyUser>();

        for (int i = 0; i < count; i++)
        {
            var user = new User
            {
                Id = ++faker.IndexVariable,
                GithubUsername = faker.Name.JobArea(),
                Name = faker.Name.FullName(),
                UserType = UserType.CommonUser
            };

            _users.Add(user);

            _recademyUsers.Add(new RecademyUser
            {
                UserId = user.Id,
            });
        }
    }

    public void Seed(ModelBuilder modelBuilder)
    {
        ArgumentNullException.ThrowIfNull(modelBuilder);

        modelBuilder.Entity<User>().HasData(_users);
        modelBuilder.Entity<RecademyUser>().HasData(_recademyUsers);
    }
}