﻿using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Recademy.Library.Dto;
using Recademy.Mock.Generators;
using Recademy.Test.Tools;

namespace Recademy.Test.Controllers
{
    public class ProjectControllerTests
    {
        private readonly TestCaseContext _testContext = new TestCaseContext();

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void AddProject_UserHasProject()
        {
            _testContext
                .WithNewUser(out UserInfoDto user)
                .WithNewProjectForUser(user, out ProjectInfoDto projectInfo);
            List<ProjectInfoDto> userProjects = _testContext.UserController.ReadUserProjects(user.Id).Value;

            Assert.True(userProjects.Any(p => p.ProjectId == projectInfo.ProjectId));
        }

        [Test]
        public void AddProjectWithTags_ProjectHasTags()
        {
            _testContext
                .WithNewUser(out UserInfoDto user)
                .WithNewProjectForUser(user, InstanceFactory.CreateAddProjectDtoWithTags(user.Id), out ProjectInfoDto projectInfo);

            UserInfoDto updatedUser = _testContext.UserController.ReadById(user.Id).Value;

            Assert.True(user.ProjectDtos.Any(p => p.ProjectSkills.Any()));
        }
    }
}