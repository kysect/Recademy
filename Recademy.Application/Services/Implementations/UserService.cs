using Recademy.Application.Mappings;
using Recademy.Application.Services.Abstractions;
using Recademy.Core.Types;
using Recademy.DataAccess;
using Recademy.Dto.Projects;
using Recademy.Dto.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using User = Recademy.Core.Models.Users.User;

namespace Recademy.Application.Services.Implementations;

public class UserService : IUserService
{
    private readonly RecademyContext _context;

    public UserService(RecademyContext context)
    {
        _context = context;
    }

    public RecademyUserDto GetById(int userId)
    {
        return _context.RecademyUsers
            .Single(user => user.UserId == userId)
            .ToDto();
    }

    public RecademyUserDto FindById(int userId)
    {
        return _context.RecademyUsers
            .SingleOrDefault(user => user.UserId == userId)
            .ToDto();
    }

    public RecademyUserDto FindRecademyUserByUsername(string username)
    {
        return _context.RecademyUsers
            .SingleOrDefault(user => user.User.GithubUsername == username)
            .ToDto();
    }

    public UserInfoDto FindUserByUsername(string username)
    {
        return _context.Users
            .SingleOrDefault(user => user.GithubUsername == username)
            .ToDto();
    }

    public IReadOnlyCollection<ProjectInfoDto> GetProjectsByUserId(int userId)
    {
        return _context.ProjectInfos
            .Where(project => project.AuthorId == userId)
            .Select(project => project.ToDto())
            .ToList();
    }

    public UserInfoDto UpdateUserRole(int adminId, int userId, UserType userType)
    {
        User admin = _context.Users.Single(user => user.Id == adminId);
        User user = _context.Users.Single(user => user.Id == userId);

        if (admin.UserType != UserType.Admin)
            throw RecademyException.NotEnoughPermission(adminId, admin.UserType, UserType.Admin);

        if (user.UserType == UserType.Admin)
            throw new RecademyException($"Cannot change role, user with id {userId} has admin role");

        if (userType == UserType.Admin)
            throw new RecademyException("Cannot set admin role. Action is not supported");

        user.UserType = userType;
        _context.SaveChanges();

        return user.ToDto();
    }

    public UserInfoDto UpdateUserPermissions(int userId, UserType permission)
    {
        User user = _context.Users.Single(user => user.Id == userId);

        user.UserType = permission;
        _context.SaveChanges();

        return user.ToDto();
    }
}